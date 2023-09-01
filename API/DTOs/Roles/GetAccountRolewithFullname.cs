using API.Utilities.Enums;

namespace API.DTOs.Roles
{
    public class GetAccountRolewithFullname
    {
        public Guid Guid { get; set; }
        public Guid AccountGuid { get; set; }
        public Guid RoleGuid { get; set; }
        public string FullName { get; set; }
        public string RoleName { get; set; }
        public string NIK { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public GenderLevel Gender { get; set; }
        public string Skill { get; set; }

    }
}
