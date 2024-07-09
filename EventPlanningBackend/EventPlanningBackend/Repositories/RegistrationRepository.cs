// Repositories/RegistrationRepository.cs
using EventPlanningBackend.Data;
using EventPlanningBackend.Models;
using EventPlanningBackend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

public class RegistrationRepository : IRegistrationRepository
{
    private readonly ApplicationDbContext _context;

    public RegistrationRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Registration> AddAsync(Registration registration)
    {
        _context.Registrations.Add(registration);
        await _context.SaveChangesAsync();
        return registration;
    }

    public async Task<Registration> GetByIdAsync(int id)
    {
        return await _context.Registrations
                             .Include(r => r.Event)
                             .Include(r => r.Account)
                             .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<Registration> GetByConfirmationTokenAsync(string token)
    {
        return await _context.Registrations
                             .Include(r => r.Event)
                             .Include(r => r.Account)
                             .FirstOrDefaultAsync(r => r.ConfirmationToken == token);
    }

    public async Task<IEnumerable<Registration>> GetRegistrationsByEventIdAsync(int eventId)
    {
        return await _context.Registrations
                             .Include(r => r.Event)
                             .Include(r => r.Account)
                             .Where(r => r.EventId == eventId)
                             .ToListAsync();
    }

    public async Task<IEnumerable<Registration>> GetRegistrationsByAccountIdAsync(int accountId)
    {
        return await _context.Registrations
                             .Include(r => r.Event)
                             .Include(r => r.Account)
                             .Where(r => r.AccountId == accountId)
                             .ToListAsync();
    }

    public async Task<Registration> UpdateAsync(Registration registration)
    {
        _context.Registrations.Update(registration);
        await _context.SaveChangesAsync();
        return registration;
    }

    public async Task DeleteAsync(int id)
    {
        var registration = await GetByIdAsync(id);
        if (registration != null)
        {
            _context.Registrations.Remove(registration);
            await _context.SaveChangesAsync();
        }
    }
}
