public class EventDto
{
    public int Id { get; set; }
    public string Name { get; set; } 
    public string Theme { get; set; } 
    public string Location { get; set; } 
    public DateTime Date { get; set; }
    public int MaxParticipants { get; set; }
    public int CurrentParticipants { get; set; }
    public List<EventAdditionalFieldDto> EventAdditionalFields { get; set; } =  new List<EventAdditionalFieldDto>();


}