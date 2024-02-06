using Clay.Domain.Entities;
using Clay.Domain.Repositories;
using Clay.Domain.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clay.Application.Services
{
    public class EventService : IEventService
    {
        private readonly IRepository<Event> _eventRepository;

        public EventService(IRepository<Event> eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task CreateEvent(string type, string description, int userId, int lockId)
        {
            var newEvent = new Event
            {
                Type = type,
                Description = description,
                UserId = userId,
                LockId = lockId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _eventRepository.CreateAsync(newEvent);
        }

        public async Task<List<Event>> ListEvents(int? lockId = null, int? userId = null)
        {
            var query = _eventRepository.GetAll();

            if (lockId.HasValue)
            {
                query = query.Where(e => e.LockId == lockId.Value);
            }

            return await query
                .Include(el => el.User)
                .Include(el => el.Lock)
                .Select(el => new Event
                {
                    ID = el.ID,
                    Type = el.Type,
                    Description = el.Description,
                    User = new User
                    {
                        Name = el.User.Name,
                    },
                    Lock = new Lock
                    {
                        Label = el.Lock.Label,
                        SerialNumber = el.Lock.SerialNumber,
                        IsLocked = el.Lock.IsLocked,
                        UnlockedAt = el.Lock.UnlockedAt,
                    },
                    CreatedAt = el.CreatedAt,
                    UpdatedAt = el.UpdatedAt,
                })
                .OrderByDescending(e => e.CreatedAt)
                .ToListAsync();
        }
    }
}
