using API.Models;
using API.Utilities.Enums;

namespace API.DTOs.Employees
{
    public class EmployeeDto
    {
        public Guid Guid { get; set; }
        public string NIK { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public GenderLevel Gender { get; set; }
        public StatusLevel Status { get; set; }
        public string? Skill { get; set; }

        public static implicit operator Employee(EmployeeDto dto)
        {
            return new Employee
            {
                Guid = dto.Guid,
                NIK = dto.NIK,
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
        public static explicit operator EmployeeDto(Employee employee)
        {
            return new EmployeeDto
            {
                Guid = employee.Guid,
                NIK = employee.NIK,
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
