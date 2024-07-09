using EventPlanningBackend.Data;
using EventPlanningBackend.Models;
using EventPlanningBackend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EventPlanningBackend.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationDbContext _context;

        public AccountRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Account> GetByIdAsync(int id)
        {
            return await _context.Accounts.Include(a => a.AccountAdditionalFields)
                                          .Include(a => a.Registrations)
                                          .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Account> AddAsync(Account account)
        {
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
            return account;
        }

        public async Task<Account> UpdateAsync(Account account)
        {
            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();
            return account;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null) return false;

            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Account> GetByEmailAsync(string email)
        {
            return await _context.Accounts.Include(a => a.AccountAdditionalFields)
                                          .Include(a => a.Registrations)
                                          .FirstOrDefaultAsync(a => a.Email == email);
        }
    }
}
