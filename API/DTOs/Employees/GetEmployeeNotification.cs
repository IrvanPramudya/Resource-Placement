namespace API.DTOs.Employees
{
    public class GetEmployeeNotification
    {
        public string ClientName { get; set; }
        public DateTime InterviewDate { get; set; }
        public string PositionName { get; set; }
        public int CapacityClient { get; set; }
        public string Note { get; set; }
    }
}
