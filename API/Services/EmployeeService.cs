using API.Contracts;
using API.Data;
using API.DTOs.AccountRoles;
using API.DTOs.Employees;
using API.Models;
using API.Repositories;
using API.Utilities.Enums;
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
        private readonly IAccountRepository _accountRepository;
        private readonly PlacementDbContext _dbContext;

        public EmployeeService(IEmployeeRepository employeeRepository, IInterviewRepository interviewRepository, IPositionRepository positionRepository, IClientRepository clientRepository, IGradeRepository gradeRepository, PlacementDbContext dbContext, IAccountRoleRepository accountRoleRepository, IAccountRepository accountRepository)
        {
            _employeeRepository = employeeRepository;
            _interviewRepository = interviewRepository;
            _positionRepository = positionRepository;
            _clientRepository = clientRepository;
            _gradeRepository = gradeRepository;
            _dbContext = dbContext;
            _accountRoleRepository = accountRoleRepository;
            _accountRepository = accountRepository;
        }
        public IEnumerable<GetReportEmployee>? GetAllEmployeeinSite()
        {
            return GetAllReportedEmployee().Where(employee => employee.Status == Utilities.Enums.StatusLevel.Site);
        }
        public IEnumerable<GetReportEmployee>? GetAllEmployeeinIdle()
        {
            return GetAllReportedEmployee().Where(employee => employee.Status == Utilities.Enums.StatusLevel.Idle && employee.InterviewDate==null);
        }
        public IEnumerable<GetEmployeeinGrade>? GetEmployeeinGrade()
        {
            var merge = 
                        from employee in _employeeRepository.GetAll()
                        join account in _accountRepository.GetAll() on employee.Guid equals account.Guid
                        join accountrole in _accountRoleRepository.GetEmployeewithEmployeeRole() on account.Guid equals accountrole.AccountGuid
                        join grade in _gradeRepository.GetAll() on employee.Guid equals grade.Guid into tbl
                        from grade in tbl.DefaultIfEmpty()
                        select new GetEmployeeinGrade
                             {
                                 Guid = employee.Guid,
                                 FullName =employee.FirstName+ " " +employee.LastName,
                                 Grade =grade!=null?grade.Name:null
                             };
            if (!merge.Any())
            {
                return null;
            }
            return merge.Where(a=>a.Grade is null);
        } 
        public IEnumerable<GetReportEmployee>? GetAllReportedEmployee()
        {
            var mergetable = from employee in _employeeRepository.GetAll()
                             join account in _accountRepository.GetAll() on employee.Guid equals account.Guid
                             join accountrole in _accountRoleRepository.GetEmployeewithEmployeeRole() on account.Guid equals accountrole.AccountGuid
                             join grade in _gradeRepository.GetAll() on employee.Guid equals grade.Guid into gradegrp
                             from grade in gradegrp.DefaultIfEmpty()
                             join interview in _interviewRepository.GetAll() on employee.Guid equals interview.Guid into interviewgrp
                             from interview in interviewgrp.DefaultIfEmpty()
                             select new GetReportEmployee
                             {
                                 EmployeeGuid = employee.Guid,
                                 NIK = employee.NIK,
                                 FullName = employee.FirstName + " " + employee.LastName,
                                 PhoneNumber = employee.PhoneNumber,
                                 Email = employee.Email,
                                 Gender = employee.Gender,
                                 Skill = employee.Skill,
                                 Grade = grade!=null?grade.Name:null,
                                 Salary = grade!=null?grade.Salary:0,
                                 Status = employee.Status,
                                 InterviewDate = interview!=null?interview.InterviewDate:null
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
                                 Salary = grade.Salary
                             };
            if (!mergetable.Any())
            {
                return null;
            }

            return mergetable.FirstOrDefault();
        }
        public GetEmployeeNotification? GetNotification(Guid guid)
        {
            var merging = from employee in _employeeRepository.GetAll()
                          join interview in _interviewRepository.GetAll() on employee.Guid equals interview.Guid
                          join client in _clientRepository.GetAll() on interview.ClientGuid equals client.Guid
                          join position in _positionRepository.GetAll() on client.Guid equals position.ClientGuid
                          where employee.Guid == guid && client.IsAvailable == true && interview.Status == InterviewLevel.EmployeeResponWaiting
                          select new GetEmployeeNotification
                          {
                              ClientGuid = client.Guid,
                              ClientName = client.Name,
                              PositionName = position.Name,
                              InterviewDate = interview.InterviewDate,
                              Note = interview.Text,
                              CreatedDate = employee.CreatedDate
                          };
            if (!merging.Any())
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
            var data = from employee in _employeeRepository.GetAll()
                       join grade in _gradeRepository.GetAll() on employee.Guid equals grade.Guid
                       join account in _accountRepository.GetAll() on employee.Guid equals account.Guid
                       join accountrole in _accountRoleRepository.GetEmployeewithEmployeeRole() on account.Guid equals accountrole.AccountGuid
                       join interview in _interviewRepository.GetAll() on employee.Guid equals interview.Guid into interviewGroup
                       from interview in interviewGroup.DefaultIfEmpty()
                       select new GetReportEmployee
                       {
                           Salary = grade.Salary,
                           Status = employee.Status,
                           InterviewDate = interview!=null?interview.InterviewDate:null,
                           ClientGuid = interview!=null?interview.ClientGuid:null,
                       };

            var countemployee = new GetCountedStatus();
            foreach (var item in data)
            {
                if (item.Status == StatusLevel.Idle)
                {
                    if(item.InterviewDate == null)
                    {
                        countemployee.CountIdleUngraded++;
                    }
                    if(item.Salary != 0 && item.InterviewDate == null)
                    {
                        countemployee.CountIdleGraded++;
                    }
                    countemployee.CountIdle++;
                }
                else if(item.Status == StatusLevel.Site)
                {
                    countemployee.CountSite++;
                }
            }
            return countemployee;
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
            toCreate.Status = 0;
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
