namespace EventPlanningBackend.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Theme { get; set; }
        public string Location { get; set; }
        public DateTime DateTime { get; set; }

        public ICollection<AdditionalField> AdditionalFields { get; set; }
        public ICollection<Registration> Registrations { get; set; }

        public Event()
        {
            AdditionalFields = new List<AdditionalField>();
            Registrations = new List<Registration>();
        }
    }

}
