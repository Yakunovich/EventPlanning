namespace EventPlanningBackend.Models
{
    public class Registration
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public int AccountId { get; set; }
        public bool IsConfirmed { get; set; }
        public string ConfirmationToken { get; set; }
        public DateTime RegistrationDate { get; set; }

        public Event Event { get; set; }
        public Account Account { get; set; }
    }

}
