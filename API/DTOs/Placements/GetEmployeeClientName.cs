namespace API.DTOs.Placements
{
    public class GetEmployeeClientName
    {
        public Guid Guid { get; set; }
        public Guid EmployeeGuid { get; set; }
        public Guid ClientGuid { get; set; }
        public Guid PositionGuid { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? EmployeeName { get; set; }
        public string? ClientName { get; set; }
        public string? PositionName { get; set; }
    }
}
