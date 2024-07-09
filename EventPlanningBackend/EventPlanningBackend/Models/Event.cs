namespace EventPlanningBackend.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Theme { get; set; }
        public string Location { get; set; }
        public DateTime Date { get; set; }
        public int? MaxParticipants { get; set; }
        public int? CurrentParticipants { get; set; }

        public ICollection<EventAdditionalField> EventAdditionalFields { get; set; } = new List<EventAdditionalField>();
        public ICollection<Registration> Registrations { get; set; } = new List<Registration>();

    }

}
