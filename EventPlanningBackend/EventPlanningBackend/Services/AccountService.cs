using AutoMapper;
using EventPlanningBackend.Models;
using EventPlanningBackend.Repositories.Interfaces;
using EventPlanningBackend.Services.Interfaces;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;
    private readonly IEmailService _emailService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AccountService(
        IAccountRepository accountRepository,
        ITokenService tokenService,
        IMapper mapper,
        IEmailService emailService,
        IHttpContextAccessor httpContextAccessor)
    {
        _accountRepository = accountRepository;
        _tokenService = tokenService;
        _mapper = mapper;
        _emailService = emailService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<string> RegisterAsync(RegisterDto registerDto)
    {
        var existingAccount = await _accountRepository.GetByEmailAsync(registerDto.Email);
        if (existingAccount != null)
        {
            return "Email already registered";
        }

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);
        var confirmationToken = Guid.NewGuid().ToString();

        var account = _mapper.Map<Account>(registerDto);
        account.Role = registerDto.Role ?? "User";
        account.PasswordHash = passwordHash;
        account.ConfirmationToken = confirmationToken;

        var request = _httpContextAccessor.HttpContext.Request;
        var baseUrl = $"{request.Scheme}://{request.Host.Value}";

        var confirmationLink = $"{baseUrl}/api/account/confirmemail?token={confirmationToken}&email={registerDto.Email}";
        var message = $"Please confirm your email by clicking the following link: <a href=\"{confirmationLink}\">Confirm Email</a>";

        await _emailService.SendEmailAsync(registerDto.Email, "Email Confirmation", message);
        await _accountRepository.AddAsync(account);

        return "Registration successful, please check your email to confirm your account.";
    }

    public async Task<string> AuthenticateAsync(LoginDto loginDto)
    {   
        var account = await _accountRepository.GetByEmailAsync(loginDto.Email);
        if (account == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, account.PasswordHash))
        {
            return null; 
        }

        if (!account.IsEmailConfirmed)
        {
            return "Email not confirmed";
        }

        return _tokenService.GenerateToken(account.Email, account.Id, account.Role);
    }

    public async Task<AccountDto> GetProfileAsync(int accountId)
    {
        var account = await _accountRepository.GetByIdAsync(accountId);
        if (account == null)
        {
            return null;  
        }

        return _mapper.Map<AccountDto>(account);
    }
    public async Task<bool> ConfirmEmailAsync(string email, string token)
    {
        var account = await _accountRepository.GetByEmailAsync(email);
        if (account == null)
        {
            throw new ApplicationException($"Unable to find account with email '{email}'.");
        }

        if (account.ConfirmationToken != token)
        {
            throw new ApplicationException("Invalid token for email confirmation.");
        }

        account.IsEmailConfirmed = true;
        await _accountRepository.UpdateAsync(account);

        return true;
    }
}
