using API.Models;
using API.Utilities.Enums;

namespace API.DTOs.Employees
{
    public class UpdateEmployeeDto
    {
        public Guid Guid { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public GenderLevel Gender { get; set; }
        public StatusLevel Status { get; set; }
        public string? Skill { get; set; }

        public static implicit operator Employee(UpdateEmployeeDto dto)
        {
            return new Employee
            {
                Guid = dto.Guid,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Status = dto.Status,
                Gender = dto.Gender,
                Skill = dto.Skill,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                ModifiedDate = DateTime.Now
            };
        }
        public static explicit operator UpdateEmployeeDto(Employee employee)
        {
            return new UpdateEmployeeDto
            {
                Guid = employee.Guid,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Status = employee.Status,
                Gender = employee.Gender,
                Skill = employee.Skill,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
            };
        }
    }
}
