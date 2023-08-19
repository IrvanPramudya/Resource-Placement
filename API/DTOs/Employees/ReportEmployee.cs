using API.Utilities.Enums;

namespace API.DTOs.Employees
{
    public class ReportEmployee
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public GenderLevel Gender { get; set; }
        public string Skill { get; set; }
        public string Grade { get; set; }
        public int Salary { get; set; }
    }
}
