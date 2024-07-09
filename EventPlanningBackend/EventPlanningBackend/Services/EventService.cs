using AutoMapper;
using EventPlanningBackend.Models;
using EventPlanningBackend.Repositories.Interfaces;

public class EventService : IEventService
{
    private readonly IEventRepository _eventRepository;
    private readonly IRegistrationRepository _registrationRepository;
    private readonly IMapper _mapper;

    public EventService(IEventRepository eventRepository, IRegistrationRepository registrationRepository, IMapper mapper)
    {
        _eventRepository = eventRepository;
        _registrationRepository = registrationRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<EventDto>> GetAllEventsAsync()
    {
        var events = await _eventRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<EventDto>>(events);
    }

    public async Task<EventDto> GetEventByIdAsync(int eventId)
    {
        var eventEntity = await _eventRepository.GetByIdAsync(eventId);
        if (eventEntity == null)
        {
            return null;
        }

        return _mapper.Map<EventDto>(eventEntity);
    }

    public async Task<EventDto> CreateEventAsync(EventDto eventDto)
    {
        var eventEntity = _mapper.Map<Event>(eventDto);
        await _eventRepository.AddAsync(eventEntity);
        return _mapper.Map<EventDto>(eventEntity);
    }

    public async Task<bool> UpdateEventAsync(int eventId, EventDto eventDto)
    {
        var eventEntity = await _eventRepository.GetByIdAsync(eventId);
        if (eventEntity == null)
        {
            return false;
        }

        _mapper.Map(eventDto, eventEntity);
        await _eventRepository.UpdateAsync(eventEntity);
        return true;
    }

    public async Task<bool> DeleteEventAsync(int eventId)
    {
        var eventEntity = await _eventRepository.GetByIdAsync(eventId);
        if (eventEntity == null)
        {
            return false;
        }

        await _eventRepository.DeleteAsync(eventEntity.Id);
        return true;
    }

    //public async Task<bool> RegisterForEventAsync(int eventId, int accountId)
    //{
    //    var eventEntity = await _eventRepository.GetByIdAsync(eventId);
    //    if (eventEntity == null || eventEntity.CurrentParticipants >= eventEntity.MaxParticipants)
    //    {
    //        return false; 
    //    }

    //    var registration = new Registration
    //    {
    //        EventId = eventId,
    //        AccountId = accountId,
    //        ConfirmationToken = Guid.NewGuid().ToString() 
    //    };

    //    eventEntity.CurrentParticipants++;
    //    await _eventRepository.UpdateAsync(eventEntity);
    //    await _registrationRepository.AddAsync(registration);
    //    return true;
    //}
}
