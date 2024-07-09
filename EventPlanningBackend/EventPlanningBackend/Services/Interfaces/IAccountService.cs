using EventPlanningBackend.Models;
public interface IAccountService
{
    Task<string> RegisterAsync(RegisterDto registerDto);
    Task<string> AuthenticateAsync(LoginDto loginDto);
    Task<AccountDto> GetProfileAsync(int accountId);
    Task<bool> ConfirmEmailAsync(string email, string token);
    //Task<bool> UpdateProfileAsync(int accountId, UpdateAccountDto updateAccountDto);
    //Task<bool> ChangePasswordAsync(int accountId, ChangePasswordDto changePasswordDto);
}