using System.Collections.Generic;
using System.Threading.Tasks;
using EventPlanningBackend.Models;

namespace EventPlanningBackend.Repositories.Interfaces
{
    public interface IRegistrationRepository
    {
        Task<Registration> AddAsync(Registration registration);
        Task<Registration> GetByIdAsync(int id);
        Task<Registration> GetByConfirmationTokenAsync(string token);
        Task<IEnumerable<Registration>> GetRegistrationsByEventIdAsync(int eventId);
        Task<IEnumerable<Registration>> GetRegistrationsByAccountIdAsync(int accountId);
        Task<Registration> UpdateAsync(Registration registration);
        Task DeleteAsync(int id);
    }
}
