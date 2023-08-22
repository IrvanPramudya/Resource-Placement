namespace API.DTOs.Grades
{
    public class GetEmployeeName
    {
        public Guid Guid { get; set; }
        public string EmployeeName { get; set; }
        public string? Grade { get; set; }
        public int? Salary { get; set; }
    }
}
