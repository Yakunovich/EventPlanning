using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class EventController : ControllerBase
{
    private readonly IEventService _eventService;

    public EventController(IEventService eventService)
    {
        _eventService = eventService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EventDto>>> GetAllEvents()
    {
        var events = await _eventService.GetAllEventsAsync();
        return Ok(events);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EventDto>> GetEventById(int id)
    {
        var eventDto = await _eventService.GetEventByIdAsync(id);
        if (eventDto == null)
        {
            return NotFound();
        }
        return Ok(eventDto);
    }

    [HttpPost]
    [Authorize(Roles = "Manager")] 
    public async Task<ActionResult<EventDto>> CreateEvent([FromBody] EventDto eventDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var createdEvent = await _eventService.CreateEventAsync(eventDto);
        return CreatedAtAction(nameof(GetEventById), new { id = createdEvent.Id }, createdEvent);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Manager")] 
    public async Task<ActionResult> UpdateEvent(int id, [FromBody] EventDto eventDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _eventService.UpdateEventAsync(id, eventDto);
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Manager")] 
    public async Task<ActionResult> DeleteEvent(int id)
    {
        var result = await _eventService.DeleteEventAsync(id);
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }

    //[HttpPost("{id}/register")]
    //[Authorize] 
    //public async Task<ActionResult> RegisterForEvent(int id)
    //{
    //    var accountId = int.Parse(User.FindFirst("AccountId").Value);
    //    var result = await _eventService.RegisterForEventAsync(id, accountId);

    //    if (!result)
    //    {
    //        return BadRequest("Unable to register for the event. It may be full or not exist.");
    //    }

    //    return Ok("Successfully registered for the event.");
    //}
}