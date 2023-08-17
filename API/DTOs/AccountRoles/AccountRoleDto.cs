using API.Models;

namespace API.DTOs.AccountRoles
{
    public class AccountRoleDto
    {
        public Guid Guid { get; set; }
        public Guid AccountGuid { get; set; }
        public Guid RoleGuid { get; set; }

        public static implicit operator AccountRole(AccountRoleDto dto)
        {
            return new AccountRole
            {
                Guid = dto.Guid,
                AccountGuid = dto.AccountGuid,
                RoleGuid = dto.RoleGuid,
                ModifiedDate = DateTime.Now
            };
        }
        public static explicit operator AccountRoleDto(AccountRole accountRole)
        {
            return new AccountRoleDto
            {
                Guid = accountRole.Guid,
                AccountGuid = accountRole.AccountGuid,
                RoleGuid = accountRole.RoleGuid,
            };
        }
    }
}
