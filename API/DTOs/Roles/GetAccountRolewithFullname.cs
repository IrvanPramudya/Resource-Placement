namespace API.DTOs.Roles
{
    public class GetAccountRolewithFullname
    {
        public Guid Guid { get; set; }
        public Guid AccountGuid { get; set; }
        public Guid RoleGuid { get; set; }

        public string FullName { get; set; }
        public string RoleName { get; set; }
    }
}
