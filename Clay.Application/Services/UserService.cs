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

            return await query.ToListAsync();
        }
    }
}
