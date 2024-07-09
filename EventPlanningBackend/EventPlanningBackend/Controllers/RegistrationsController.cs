using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class RegistrationController : ControllerBase
{
    private readonly IRegistrationService _registrationService;

    public RegistrationController(IRegistrationService registrationService)
    {
        _registrationService = registrationService;
    }

    [HttpPost]
    //[Authorize]
    public async Task<IActionResult> Register([FromBody] RegistrationDto request)
    {
        var confirmationToken = Guid.NewGuid().ToString();
        var registration = await _registrationService.RegisterAsync(request.EventId, request.AccountId, confirmationToken);

        return Ok(registration);
    }

    [HttpGet("confirm/{token}")]
    public async Task<IActionResult> ConfirmRegistration(string token)
    {
        var success = await _registrationService.ConfirmRegistrationAsync(token);
        if (!success)
        {
            return BadRequest("Invalid or expired confirmation token.");
        }

        return Ok("Registration confirmed.");
    }

    [HttpGet("event/{eventId}")]
    public async Task<IActionResult> GetRegistrationsByEventId(int eventId)
    {
        var registrations = await _registrationService.GetRegistrationsByEventIdAsync(eventId);
        return Ok(registrations);
    }

    [HttpGet("account/{accountId}")]
    public async Task<IActionResult> GetRegistrationsByAccountId(int accountId)
    {
        var registrations = await _registrationService.GetRegistrationsByAccountIdAsync(accountId);
        return Ok(registrations);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> CancelRegistration(int id)
    {
        var success = await _registrationService.CancelRegistrationAsync(id);
        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }
}
