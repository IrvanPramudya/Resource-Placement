using API.Models;

namespace API.DTOs.Roles
{
    public class InsertRoleDefaultDto
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }

        public static implicit operator Role(InsertRoleDefaultDto insertRoleDto)
        {
            return new Role
            {
                Guid = insertRoleDto.Guid,
                Name = insertRoleDto.Name,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
        }
        public static explicit operator InsertRoleDefaultDto(Role role)
        {
            return new InsertRoleDefaultDto
            {
                Name = role.Name,
            };
        }
    }
}
