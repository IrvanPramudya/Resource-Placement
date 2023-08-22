using API.Contracts;
using API.DTOs.Placements;
using API.Models;
using API.Repositories;

namespace API.Services
{
    public class PlacementService
    {
        private readonly IPlacementRepository _placementRepository;
<<<<<<< HEAD
        private readonly IClientRepository _clientRepository;
=======
<<<<<<< Updated upstream
>>>>>>> mais-branch

        public PlacementService(IPlacementRepository placementRepository, IClientRepository clientRepository)
        {
            _placementRepository = placementRepository;
<<<<<<< HEAD
            _clientRepository = clientRepository;
=======
=======
        private readonly IClientRepository _clientRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public PlacementService(IPlacementRepository placementRepository, IClientRepository clientRepository, IEmployeeRepository employeeRepository)
        {
            _placementRepository = placementRepository;
            _clientRepository = clientRepository;
            _employeeRepository = employeeRepository;
        }

        public IEnumerable<GetEmployeeClientName> GetEmployeeClientName()
        {
            var merge = from e in _employeeRepository.GetAll()
                        join p in _placementRepository.GetAll() on e.Guid equals p.EmployeeGuid
                        join c in _clientRepository.GetAll() on p.ClientGuid equals c.Guid
                        select new GetEmployeeClientName
                        {
                            Guid = p.Guid,
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
>>>>>>> mais-branch
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
<<<<<<< HEAD
=======
>>>>>>> Stashed changes
>>>>>>> mais-branch
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
            var placement = _placementRepository.Create(newPlacementDto);
            if (placement is null)
            {
                return null; // Placement is null or not found;
            }

            return (PlacementDto)placement; // Placement is found;
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

            var result = _placementRepository.Delete(placement);

            return result ? 1 // Placement is deleted;
                : 0; // Placement failed to delete;
        }
    }
}
