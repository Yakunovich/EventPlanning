public class RegisterDto
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string FullName { get; set; }
    public string Role { get; set; }
    public List<EventAdditionalFieldDto> AdditionalFields { get; set; } = new List<EventAdditionalFieldDto>();
}