using EventPlanningBackend.Models;
using EventPlanningBackend.Repositories.Interfaces;

public class RegistrationService : IRegistrationService
{
    private readonly IRegistrationRepository _registrationRepository;
    private readonly IEventRepository _eventRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IEmailService _emailService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public RegistrationService(
        IRegistrationRepository registrationRepository,
        IEventRepository eventRepository,
        IAccountRepository accountRepository,
        IEmailService emailService,
        IHttpContextAccessor httpContextAccessor)
    {
        _registrationRepository = registrationRepository;
        _eventRepository = eventRepository;
        _accountRepository = accountRepository;
        _emailService = emailService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Registration> RegisterAsync(int eventId, int accountId, string confirmationToken)
    {
        var eventEntity = await _eventRepository.GetByIdAsync(eventId);
        if (eventEntity == null)
        {
            throw new Exception("Event not found.");
        }

        var account = await _accountRepository.GetByIdAsync(accountId);
        if (account == null)
        {
            throw new Exception("Account not found.");
        }

        var currentRegistrations = await _registrationRepository.GetRegistrationsByEventIdAsync(eventId);
        if (eventEntity.MaxParticipants.HasValue && currentRegistrations.Count() >= eventEntity.MaxParticipants.Value)
        {
            throw new Exception("Event has reached the maximum number of participants.");
        }

        var registration = new Registration
        {
            EventId = eventId,
            AccountId = accountId,
            ConfirmationToken = confirmationToken,
            IsConfirmed = false,
            RegistrationDate = DateTime.UtcNow
        };

        await _registrationRepository.AddAsync(registration);


        var request = _httpContextAccessor.HttpContext.Request;
        var baseUrl = $"{request.Scheme}://{request.Host.Value}";


        var confirmationLink = $"{baseUrl}/api/registration/confirm/{confirmationToken}";
        var message = $"Please confirm your event registration by clicking the following link: <a href=\"{confirmationLink}\">Confirm Registration</a>";

        await _emailService.SendEmailAsync(account.Email, "Event Registration Confirmation", message);


        return registration;
    }

    public async Task<bool> ConfirmRegistrationAsync(string token)
    {
        var registration = await _registrationRepository.GetByConfirmationTokenAsync(token);
        if (registration == null || registration.IsConfirmed)
        {
            return false;
        }

        registration.IsConfirmed = true;
        await _registrationRepository.UpdateAsync(registration);

        var eventEntity = await _eventRepository.GetByIdAsync(registration.EventId);
        eventEntity.CurrentParticipants += 1;
        await _eventRepository.UpdateAsync(eventEntity);

        return true;
    }

    public async Task<IEnumerable<Registration>> GetRegistrationsByEventIdAsync(int eventId)
    {
        return await _registrationRepository.GetRegistrationsByEventIdAsync(eventId);
    }

    public async Task<IEnumerable<Registration>> GetRegistrationsByAccountIdAsync(int accountId)
    {
        return await _registrationRepository.GetRegistrationsByAccountIdAsync(accountId);
    }

    public async Task<bool> CancelRegistrationAsync(int registrationId)
    {
        var registration = await _registrationRepository.GetByIdAsync(registrationId);
        if (registration == null)
        {
            return false;
        }

        await _registrationRepository.DeleteAsync(registrationId);
        return true;
    }
}
