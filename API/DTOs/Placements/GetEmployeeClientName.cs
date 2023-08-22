namespace API.DTOs.Placements
{
    public class GetEmployeeClientName
    {
        public Guid Guid { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? EmployeeName { get; set; }
        public string? ClientName { get; set; }
    }
}
