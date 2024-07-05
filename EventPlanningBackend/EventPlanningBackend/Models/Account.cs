namespace EventPlanningBackend.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public ICollection<Registration> Registrations { get; set; }

        public Account()
        {
            Registrations = new List<Registration>();
        }
    }

}
