namespace EventPlanningBackend.Models
{
    public class EventAdditionalField
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }
        public string FieldName { get; set; }
        public string FieldValue { get; set; }
    }

}
