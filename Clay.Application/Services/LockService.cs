using Clay.Domain.Entities;
using Clay.Domain.Repositories;
using Clay.Domain.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clay.Application.Services
{
    public class LockService: ILockService
    {
        private readonly IRepository<Lock> _lockRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IEventService _eventService;
        public LockService(IRepository<Lock> lockRepository, IRepository<User> userRepository, IEventService eventService)
        {
            _lockRepository = lockRepository;
            _userRepository = userRepository;
            _eventService = eventService;
        }

        public async Task UpdateLockStatus(int id, bool lockStatus, int userId)
        {
            var doorLock = await _lockRepository.GetByIdAsync(id);

            doorLock.IsLocked = lockStatus;
            
            if(lockStatus == false)
            {
                doorLock.UnlockedAt = DateTime.UtcNow;
            }

            var lockStatusString = lockStatus ? "Lock" : "Unlock";

            await _eventService.CreateEvent(
                    lockStatusString,
                    $"Door {doorLock.SerialNumber} was {lockStatusString}ed",
                    userId,
                    id
                );
        }

        public bool CanUserAccessLock(int userId, int lockId)
        {
            var user = _userRepository.GetAll().Include(user => user.UserRoles).ThenInclude(userRoles => userRoles.Role).FirstOrDefault(user => user.ID == userId);
            var lockInstance = _lockRepository.GetAll().Include(lockInstance => lockInstance.LockRoles).ThenInclude(lockRoles => lockRoles.Role).FirstOrDefault(lockInstance => lockInstance.ID == lockId);

            if(user == null || lockInstance == null)
            {
                return false;
            }

            return user.UserRoles.Any(userRole => lockInstance.LockRoles.Any(lockRole => lockRole.RoleId == userRole.RoleId));
        }

        public async Task<bool> LockExists(int lockId)
        {
            return await _lockRepository.GetAll().AnyAsync(el => el.ID == lockId);
        }

        public async Task AutoLockDoors()
        {
            var timeNow = DateTime.Now;

            var locks = await _lockRepository.GetAll().Where(l => !l.IsLocked && l.ShouldLockAfter.HasValue).Where(l => l.UnlockedAt.Value.AddSeconds(l.ShouldLockAfter.Value) <= timeNow).ToListAsync();
            
            locks.ForEach(l => l.IsLocked = true);

            await _lockRepository.UpdateRangeAsync(locks);
        }
    }
}
