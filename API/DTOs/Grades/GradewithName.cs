using API.Utilities.Enums;

namespace API.DTOs.Grades
{
    public class GradewithName
    {
        public Guid Guid { get; set; }
        public string EmployeeName { get; set; }
        public string? GradeName { get; set; }
        public int Salary { get; set; }
        public string NIK { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public GenderLevel Gender { get; set; }
        public string Skill { get; set; }
        public StatusLevel Status { get; set; }

    }
}
