using Clay.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clay.Domain.Services
{
    public interface IEventService
    {
        Task CreateEvent(string type, string description, int userId, int lockId);
        Task<List<Event>> ListEvents(int? lockId = null, int? userId = null);
    }
}