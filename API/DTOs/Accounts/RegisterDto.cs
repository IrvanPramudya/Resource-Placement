using API.Utilities.Enums;

namespace API.DTOs.Accounts
{
    public class RegisterDto
    {
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public GenderLevel Gender { get; set; }
        public string? Skill { get; set; }
    }
}
