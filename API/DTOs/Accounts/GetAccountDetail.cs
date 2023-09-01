using API.Utilities.Enums;

namespace API.DTOs.Accounts
{
    public class GetAccountDetail
    {
        //Employee
        public string FullName { get; set; }
        public string Email { get; set; }
        public string NIK { get; set; }
        public string PhoneNumber { get; set; }
        public GenderLevel Gender { get; set; }
        public StatusLevel Status { get; set; }
        public string Skill { get; set; }
        //Role
        public string? RoleName { get; set; }
        //Grade
        public string? GradeName { get; set; }
        public int? Salary { get; set; }
        //Interview
        public DateTime? InterviewDate { get; set; }
        public string? ClientInterviewName { get; set; }
        public string? PositionInterviewName { get; set; }
        //Placement
        public int Contract { get; set; }
        public string? ClientPlacementName { get; set; }
        public string? PositionPlacementName { get; set; }


    }
}
