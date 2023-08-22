using API.Contracts;
using API.Data;
using API.DTOs.AccountRoles;
using API.DTOs.Employees;
using API.Models;
using API.Repositories;
using API.Utilities.Handlers;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;
using System.Security.Claims;
using System.Security.Principal;

namespace API.Services
{
    public class EmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IInterviewRepository _interviewRepository;
        private readonly IPositionRepository _positionRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IGradeRepository _gradeRepository;
        private readonly IAccountRoleRepository _accountRoleRepository;
        private readonly PlacementDbContext _dbContext;

        public EmployeeService(IEmployeeRepository employeeRepository, IInterviewRepository interviewRepository, IPositionRepository positionRepository, IClientRepository clientRepository, IGradeRepository gradeRepository, PlacementDbContext dbContext, IAccountRoleRepository accountRoleRepository)
        {
            _employeeRepository = employeeRepository;
            _interviewRepository = interviewRepository;
            _positionRepository = positionRepository;
            _clientRepository = clientRepository;
            _gradeRepository = gradeRepository;
            _dbContext = dbContext;
            _accountRoleRepository = accountRoleRepository;
        }
        /*public int InsertReportEmployee(ReportEmployee inputed)
        {
            if (!_employeeRepository.IsNotExist(inputed.Email)
               || !_employeeRepository.IsNotExist(inputed.PhoneNumber))
            {
                return 0;
            }
            using var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                var NewNik = GenerateHandler.LastNik(_employeeRepository.GetLastNik());
                var employee = _employeeRepository.Create(new Employee
                {
                    Guid = new Guid(),
                    NIK = NewNik,
                    FirstName = inputed.FirstName,
                    LastName = inputed.LastName,
                    Email = inputed.Email,
                    PhoneNumber = inputed.PhoneNumber,
                    Gender = inputed.Gender,
                    Status = 0,
                    Skill = inputed.Skill,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                }) ;
                var grade = _gradeRepository.Create(new Grade
                {
                    Guid = employee.Guid,
                    Name =inputed.Grade,
                    Salary = inputed.Salary,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                });
                var accountroleadmin = _accountRoleRepository.Create(new NewAccountRoleDto
                {
                    AccountGuid = employee.Guid,
                    RoleGuid = Guid.Parse("ae259a90-e2e8-442f-ce18-08db91a71ab9")
                });
                transaction.Commit();
                return 1;
            }
            catch
            {
                transaction.Rollback();
                return -1;
            }
        }*/
        public IEnumerable<GetReportEmployee>? GetAllReportedEmployee()
        {
            var mergetable = from employee in _employeeRepository.GetAll()
                             join grade in _gradeRepository.GetAll() on employee.Guid equals grade.Guid
                             select new GetReportEmployee
                             {
                                NIK = employee.NIK,
                                FullName = employee.FirstName + " " + employee.LastName,
                                PhoneNumber = employee.PhoneNumber,
                                Email = employee.Email,
                                Gender = employee.Gender,
                                Skill = employee.Skill,
                                Grade = grade.Name,
                                Salary  = grade.Salary
                             };
            if (!mergetable.Any())
            {
                return null;
            }
            return mergetable;
        }
        public GetReportEmployee? GetReportedEmployee(Guid guid)
        {
            var mergetable = from employee in _employeeRepository.GetAll()
                             join grade in _gradeRepository.GetAll() on employee.Guid equals grade.Guid
                             where employee.Guid == guid
                             select new GetReportEmployee
                             {
                                NIK = employee.NIK,
                                FullName = employee.FirstName + " " + employee.LastName,
                                PhoneNumber = employee.PhoneNumber,
                                Email = employee.Email,
                                Gender = employee.Gender,
                                Skill = employee.Skill,
                                Grade = grade.Name,
                                Salary  = grade.Salary
                             };
            if (!mergetable.Any())
            {
                return null;
            }

            return mergetable.FirstOrDefault();
        }
        public GetEmployeeNotification? GetNotification(Guid guid)
        {
            var merging = from employee     in _employeeRepository.GetAll()
                          join interview    in _interviewRepository.GetAll() on employee.Guid equals interview.Guid
                          join client       in _clientRepository.GetAll() on interview.ClientGuid equals client.Guid
                          join position     in _positionRepository.GetAll() on client.Guid equals position.ClientGuid
                          where employee.Guid == guid && client.IsAvailable == true
                          select new GetEmployeeNotification
                          {
                              ClientName = client.Name,
                              PositionName  = position.Name,
                              CapacityClient = client.Capacity,
                              InterviewDate = interview.InterviewDate,
                              Note = interview.Text
                          };
            if(!merging.Any())
            {
                return null;
            }
            return merging.FirstOrDefault();
        }
        public int CountEmployee()
        {
            var data = _employeeRepository.GetAll();
            var counter = 0;
            foreach (var item in data)
            {
                counter++;
            }
            return counter;
        }

        public GetCountedGender? CountGender()
        {
            var data = _employeeRepository.GetAll();
            var countfemale = 0;
            var countmale = 0;

            foreach (var item in data)
            {
                if (item.Gender == 0)
                {
                    countfemale++;
                }
                else
                {
                    countmale++;
                }
            }
            return new GetCountedGender
            {
                CountFemale = countfemale,
                CountMale = countmale
            };
        }

        public GetCountedStatus? CountStatus()
        {
            var data = _employeeRepository.GetAll();
            var countidle = 0;
            var countsite = 0;

            foreach (var item in data)
            {
                if (item.Status == 0)
                {
                    countidle++;
                }
                else
                {
                    countsite++;
                }
            }
            return new GetCountedStatus
            {
                CountIdle = countidle,
                CountSite = countsite
            };
        }

        
        public IEnumerable<EmployeeDto> GetAll()
        {
            var employees = _employeeRepository.GetAll();
            if (!employees.Any())
            {
                return Enumerable.Empty<EmployeeDto>(); // Employee is null or not found;
            }

            var employeeDtos = new List<EmployeeDto>();
            foreach (var employee in employees)
            {
                employeeDtos.Add((EmployeeDto)employee);
            }

            return employeeDtos; // Employee is found;
        }

        public EmployeeDto? GetByGuid(Guid guid)
        {
            var employee = _employeeRepository.GetByGuid(guid);
            if (employee is null)
            {
                return null; // Employee is null or not found;
            }

            return (EmployeeDto)employee; // Employee is found;
        }

        public EmployeeDto? Create(NewEmployeeDto newEmployeeDto)
        {
            Employee toCreate = newEmployeeDto;
            toCreate.NIK = GenerateHandler.LastNik(_employeeRepository.GetLastNik());

            var employee = _employeeRepository.Create(toCreate);
            if (employee is null)
            {
                return null; // Employee is null or not found;
            }

            return (EmployeeDto)employee; // Employee is found;
        }

        public int Update(UpdateEmployeeDto employeeDto)
        {
            var employee = _employeeRepository.GetByGuid(employeeDto.Guid);
            if (employee is null)
            {
                return -1; // Employee is null or not found;
            }

            Employee toUpdate = employeeDto;
            toUpdate.NIK = employee.NIK;
            toUpdate.CreatedDate = employee.CreatedDate;
            var result = _employeeRepository.Update(toUpdate);

            return result ? 1 // Employee is updated;
                : 0; // Employee failed to update;
        }

        public int Delete(Guid guid)
        {
            var employee = _employeeRepository.GetByGuid(guid);
            if (employee is null)
            {
                return -1; // Employee is null or not found;
            }

            var result = _employeeRepository.Delete(employee);

            return result ? 1 // Employee is deleted;
                : 0; // Employee failed to delete;
        }
    }
}
