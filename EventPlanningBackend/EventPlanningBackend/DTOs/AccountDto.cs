﻿public class AccountDto
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string FullName { get; set; }
    public string Role { get; set; }
    public List<AccountAdditionalFieldDto> AccountAdditionalFields { get; set; } = new List<AccountAdditionalFieldDto>();
}