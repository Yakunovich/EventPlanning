using System.Threading.Tasks;
using EventPlanningBackend.Models;

namespace EventPlanningBackend.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        Task<Account> GetByIdAsync(int id);
        Task<Account> AddAsync(Account account);
        Task<Account> UpdateAsync(Account account);
        Task<bool> DeleteAsync(int id);
        Task<Account> GetByEmailAsync(string email);
    }
}
