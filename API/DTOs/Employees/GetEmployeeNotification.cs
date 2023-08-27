namespace API.DTOs.Employees
{
    public class GetEmployeeNotification
    {
        public Guid ClientGuid { get; set; }
        public string ClientName { get; set; }
        public DateTime InterviewDate { get; set; }
        public string PositionName { get; set; }
        public string Note { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
