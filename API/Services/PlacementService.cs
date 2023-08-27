﻿using API.Contracts;
using API.Data;
using API.DTOs.Employees;
using API.DTOs.Placements;
using API.Models;
using API.Repositories;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class PlacementService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IPlacementRepository _placementRepository;
        private readonly IInterviewRepository _interviewRepository;
        private readonly PlacementDbContext _dbContext;

        public PlacementService(IPlacementRepository placementRepository, IClientRepository clientRepository, IEmployeeRepository employeeRepository, IInterviewRepository interviewRepository, PlacementDbContext dbContext)
        {
            _placementRepository = placementRepository;
            _clientRepository = clientRepository;
            _employeeRepository = employeeRepository;
            _interviewRepository = interviewRepository;
            _dbContext = dbContext;
        }
        public IEnumerable<GetEmployeeClientName> GetEmployeeClientName()
        {
            var merge = from e in _employeeRepository.GetAll()
                        join p in _placementRepository.GetAll() on e.Guid equals p.Guid
                        join c in _clientRepository.GetAll() on p.ClientGuid equals c.Guid
                        select new GetEmployeeClientName
                        {
                            Guid = p.Guid,
                            EmployeeGuid = e.Guid,
                            ClientGuid = c.Guid,
                            StartDate = p.StartDate,
                            EndDate = p.EndDate,
                            EmployeeName = e.FirstName + " " +e.LastName,
                            ClientName = c.Name
                            
                        };
            if (!merge.Any())
            {
                return null;
            }
            return merge;
        }
        public IEnumerable<GetCountedClient> GetCountedClient()
        {
            var placement = _placementRepository.GetAll();
            var client = _clientRepository.GetAll();
            var listclient = new List<GetCountedClient>();
            foreach (var item in client)
            {
                var newclient = new GetCountedClient
                {
                    ClientName = item.Name,
                    CountEmployee = 0
                };
                foreach(var item2 in placement)
                {
                    if(item.Guid == item2.ClientGuid)
                    {
                        newclient.CountEmployee++;
                    }
                }
                listclient.Add(newclient);
            }
            return listclient;
        }
        public IEnumerable<PlacementDto> GetAll()
        {
            var placements = _placementRepository.GetAll();
            if (!placements.Any())
            {
                return Enumerable.Empty<PlacementDto>(); // Placement is null or not found;
            }

            var placementDtos = new List<PlacementDto>();
            foreach (var placement in placements)
            {
                placementDtos.Add((PlacementDto)placement);
            }

            return placementDtos; // Placement is found;
        }

        public PlacementDto? GetByGuid(Guid guid)
        {
            var placement = _placementRepository.GetByGuid(guid);
            if (placement is null)
            {
                return null; // Placement is null or not found;
            }

            return (PlacementDto)placement; // Placement is found;
        }

        public PlacementDto? Create(NewPlacementDto newPlacementDto)
        {
            var interview = _interviewRepository.GetByGuid(newPlacementDto.Guid);
            if(interview is null || interview.Status != Utilities.Enums.InterviewLevel.AcceptedbyClient)
            {
                return null;
            }
            using var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                var placement = _placementRepository.Create(newPlacementDto);
                if (placement is null)
                {
                    return null; // Placement is null or not found;
                }
                var employee = _employeeRepository.GetByGuid(newPlacementDto.Guid);
                if (employee is null)
                {
                    return null; // employee is null or not found;
                }
                var employeeUpdate = new UpdateEmployeeDto
                {
                    Guid = newPlacementDto.Guid,
                    Email = employee.Email,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Gender = employee.Gender,
                    PhoneNumber = employee.PhoneNumber,
                    Skill = employee.Skill,
                    Status = Utilities.Enums.StatusLevel.Site
                };
                Employee employeeToUpdate = employeeUpdate;
                employeeToUpdate.NIK = employee.NIK;
                employeeToUpdate.CreatedDate = employee.CreatedDate;
                var UpdatedEmployee = _employeeRepository.Update(employeeToUpdate);
                transaction.Commit();
                return (PlacementDto)placement; // Placement is found;
            }
            catch
            {
                transaction.Rollback();
                return null;
            }
            
        }

        public int Update(PlacementDto placementDto)
        {
            var placement = _placementRepository.GetByGuid(placementDto.Guid);
            if (placement is null)
            {
                return -1; // Placement is null or not found;
            }

            Placement toUpdate = placementDto;
            toUpdate.CreatedDate = placement.CreatedDate;
            var result = _placementRepository.Update(toUpdate);

            return result ? 1 // Placement is updated;
                : 0; // Placement failed to update;
        }

        public int Delete(Guid guid)
        {
            
            var placement = _placementRepository.GetByGuid(guid);
            if (placement is null)
            {
                return -1; // Placement is null or not found;
            }
            var employee = _employeeRepository.GetByGuid(placement.Guid);
            employee.Status = 0;
            var employeeUpdate = _employeeRepository.Update(employee);
            var result = _placementRepository.Delete(placement);

            return result ? 1 // Placement is deleted;
                : 0; // Placement failed to delete;
        }
    }
}
