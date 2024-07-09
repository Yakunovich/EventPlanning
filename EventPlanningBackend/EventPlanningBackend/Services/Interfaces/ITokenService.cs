namespace EventPlanningBackend.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(string email, int accountId, string role);
    }
}
