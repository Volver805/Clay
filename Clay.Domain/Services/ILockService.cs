using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clay.Domain.Services
{
    public interface ILockService
    {
        public Task UpdateLockStatus(int id, bool lockStatus, int userId);
        public bool CanUserAccessLock(int userId, int lockId);
        public Task<bool> LockExists(int lockId);
        public Task AutoLockDoors();
    }
}
