using System.Collections.Generic;
using System.Threading.Tasks;
using EventPlanningBackend.Models;

namespace EventPlanningBackend.Repositories.Interfaces
{
    public interface IEventRepository
    {
        Task<IEnumerable<Event>> GetAllAsync();
        Task<Event> GetByIdAsync(int id);
        Task<Event> AddAsync(Event eventEntity);
        Task<Event> UpdateAsync(Event eventEntity);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsByNameAsync(string name);
    }
}