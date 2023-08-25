using API.Models;
using API.Utilities.Enums;

namespace API.DTOs.Employees
{
    public class NewEmployeeDto
    {
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public GenderLevel Gender { get; set; }
        public string? Skill { get; set; }

        public static implicit operator Employee(NewEmployeeDto dto)
        {
            return new Employee
            {
                Guid = new Guid(),
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Gender = dto.Gender,
                Skill = dto.Skill,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
            };
        }
        public static explicit operator NewEmployeeDto(Employee employee)
        {
            return new NewEmployeeDto
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Gender = employee.Gender,
                Skill = employee.Skill,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
            };
        }
    }
}
