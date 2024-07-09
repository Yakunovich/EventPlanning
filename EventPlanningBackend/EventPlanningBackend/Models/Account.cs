namespace EventPlanningBackend.Models
{
    public class Account 
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string ConfirmationToken { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public ICollection<AccountAdditionalField> AccountAdditionalFields { get; set; } = new List<AccountAdditionalField>();
        public ICollection<Registration> Registrations { get; set; } = new List<Registration>();
    }

}
