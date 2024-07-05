namespace EventPlanningBackend.Models
{
    public class Registration
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }
        public string AdditionalInfo { get; set; }
    }

}
