
using EventPlanningBackend.Data;
using EventPlanningBackend.Models;
using EventPlanningBackend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

public class EventRepository : IEventRepository
{
    private readonly ApplicationDbContext _context;

    public EventRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Event>> GetAllAsync()
    {
        return await _context.Events.Include(e => e.EventAdditionalFields).ToListAsync();
    }

    public async Task<Event> GetByIdAsync(int id)
    {
        return await _context.Events.Include(e => e.EventAdditionalFields)
                                    .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<Event> AddAsync(Event eventEntity)
    {
        _context.Events.Add(eventEntity);
        await _context.SaveChangesAsync();
        return eventEntity;
    }

    public async Task<Event> UpdateAsync(Event eventEntity)
    {
        _context.Events.Update(eventEntity);
        await _context.SaveChangesAsync();
        return eventEntity;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var eventEntity = await _context.Events.FindAsync(id);
        if (eventEntity == null) return false;

        _context.Events.Remove(eventEntity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await _context.Events.AnyAsync(e => e.Name == name);
    }
}
