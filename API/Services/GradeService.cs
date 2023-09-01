using API.Contracts;
using API.DTOs.Grades;
using API.Models;

namespace API.Services
{
    public class GradeService
    {
        private readonly IGradeRepository _gradeRepository;

<<<<<<< Updated upstream
        public GradeService(IGradeRepository gradeRepository)
        {
            _gradeRepository = gradeRepository;
=======
        public GradeService(IGradeRepository gradeRepository, IEmployeeRepository employeeRepository, IAccountRepository accountRepository, IAccountRoleRepository accountRoleRepository)
        {
            _gradeRepository = gradeRepository;
            _employeeRepository = employeeRepository;
            _accountRepository = accountRepository;
            _accountRoleRepository = accountRoleRepository;
        }
        public CountEmployee CountEmployeeinGrade()
        {
            var grade = GetAllEmployeewithGrade();
            var newgrade = new CountEmployee();
            foreach (var item in grade)
            {
                if (item.GradeName == null)
                {
                    newgrade.CountUngraded++;
                }
                else
                {
                    newgrade.CountGraded++;
                }
            }
            return newgrade;
        }
        public IEnumerable<GradewithName>? GetAllEmployeewithGrade()
        {

            var merge = from employee in _employeeRepository.GetAll()
                        join grade in _gradeRepository.GetAll() on employee.Guid equals grade.Guid into gradegroup
                        from grade in gradegroup.DefaultIfEmpty()
                        join account in _accountRepository.GetAll() on employee.Guid equals account.Guid
                        join accountrole in _accountRoleRepository.GetEmployeewithEmployeeRole() on account.Guid equals accountrole.AccountGuid
                        
                        select new GradewithName
                        {
                            Guid = employee.Guid,
                            EmployeeName = employee.FirstName + " " + employee.LastName,
                            GradeName = grade != null ? grade.Name : null,
                            Salary = grade != null ? grade.Salary : 0,
                            Email = employee.Email,
                            Gender = employee.Gender,
                            NIK = employee.NIK,
                            PhoneNumber = employee.PhoneNumber,
                            Skill = employee.Skill
                        };
            if (!merge.Any())
            {
                return null;
            }
            return merge;
        }
        public IEnumerable<GradewithName>? GetwithName()
        {
            var merge = from employee in _employeeRepository.GetAll()
                        join grade in _gradeRepository.GetAll() on employee.Guid equals grade.Guid
                        select new GradewithName
                        {
                            Guid = employee.Guid,
                            EmployeeName = employee.FirstName + " " + employee.LastName,
                            GradeName = grade.Name,
                            Salary = grade.Salary,
                        };
            if (!merge.Any())
            {
                return null;
            }
            return merge;
>>>>>>> Stashed changes
        }

        public IEnumerable<GradeDto> GetAll()
        {
            var grades = _gradeRepository.GetAll();
            if (!grades.Any())
            {
                return Enumerable.Empty<GradeDto>(); // Grade is null or not found;
            }

            var gradeDtos = new List<GradeDto>();
            foreach (var grade in grades)
            {
                gradeDtos.Add((GradeDto)grade);
            }

            return gradeDtos; // Grade is found;
        }

        public GradeDto? GetByGuid(Guid guid)
        {
            var grade = _gradeRepository.GetByGuid(guid);
            if (grade is null)
            {
                return null; // Grade is null or not found;
            }

            return (GradeDto)grade; // Grade is found;
        }

        public GradeDto? Create(NewGradeDto newGradeDto)
        {
            var grade = _gradeRepository.Create(newGradeDto);
            if (grade is null)
            {
                return null; // Grade is null or not found;
            }

            return (GradeDto)grade; // Grade is found;
        }

        public int Update(GradeDto gradeDto)
        {
            var grade = _gradeRepository.GetByGuid(gradeDto.Guid);
            if (grade is null)
            {
                return -1; // Grade is null or not found;
            }

            Grade toUpdate = gradeDto;
            toUpdate.CreatedDate = grade.CreatedDate;
            var result = _gradeRepository.Update(toUpdate);

            return result ? 1 // Grade is updated;
                : 0; // Grade failed to update;
        }

        public int Delete(Guid guid)
        {
            var grade = _gradeRepository.GetByGuid(guid);
            if (grade is null)
            {
                return -1; // Grade is null or not found;
            }

            var result = _gradeRepository.Delete(grade);

            return result ? 1 // Grade is deleted;
                : 0; // Grade failed to delete;
        }
    }
}
