namespace EventPlanningBackend.Models
{
    public class AccountAdditionalField
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }
        public string FieldName { get; set; }
        public string FieldValue { get; set; }
    }
}
