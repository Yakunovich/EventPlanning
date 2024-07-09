using EventPlanningBackend.Models;

public interface IRegistrationService
{
    Task<Registration> RegisterAsync(int eventId, int accountId, string confirmationToken);
    Task<bool> ConfirmRegistrationAsync(string token);
    Task<IEnumerable<Registration>> GetRegistrationsByEventIdAsync(int eventId);
    Task<IEnumerable<Registration>> GetRegistrationsByAccountIdAsync(int accountId);
    Task<bool> CancelRegistrationAsync(int registrationId);
}