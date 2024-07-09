using EventPlanningBackend.Models;

public interface IEventService
{
    Task<IEnumerable<EventDto>> GetAllEventsAsync();
    Task<EventDto> GetEventByIdAsync(int eventId);
    Task<EventDto> CreateEventAsync(EventDto eventDto);
    Task<bool> UpdateEventAsync(int eventId, EventDto eventDto);
    Task<bool> DeleteEventAsync(int eventId);
    //Task<bool> RegisterForEventAsync(int eventId, int accountId);
}
