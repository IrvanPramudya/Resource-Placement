namespace API.DTOs.AccountRoles
{
    public class GetEmployeehasAccount
    {
        public Guid? AccountGuid { get; set; }
        public Guid EmployeeGuid { get; set; }
        public Guid? RoleGuid { get; set; }
        public string FullName { get; set; }


    }
}
