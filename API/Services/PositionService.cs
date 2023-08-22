using API.Contracts;
using API.DTOs.Positions;
using API.Models;
using API.Repositories;

namespace API.Services
{
    public class PositionService
    {
        private readonly IPositionRepository _positionRepository;
        private readonly IClientRepository _clientRepository;

        public PositionService(IPositionRepository positionRepository, IClientRepository clientRepository)
        {
            _positionRepository = positionRepository;
            _clientRepository = clientRepository;
        }

        public IEnumerable<GetClientName> GetClientName()
        {
            var merge = from client in _clientRepository.GetAll()
                        join position in _positionRepository.GetAll() on client.Guid equals position.ClientGuid
                        select new GetClientName
                        {
                            Guid = position.Guid,
                            ClientName = client.Name,
                            PositionName = position.Name
                        };
            if(!merge.Any() )
            {
                return null;
            }
            return merge;
        }

        public IEnumerable<PositionDto> GetAll()
        {
            var positions = _positionRepository.GetAll();
            if (!positions.Any())
            {
                return Enumerable.Empty<PositionDto>(); // Position is null or not found;
            }

            var positionDtos = new List<PositionDto>();
            foreach (var position in positions)
            {
                positionDtos.Add((PositionDto)position);
            }

            return positionDtos; // Position is found;
        }

        public PositionDto? GetByGuid(Guid guid)
        {
            var position = _positionRepository.GetByGuid(guid);
            if (position is null)
            {
                return null; // Position is null or not found;
            }

            return (PositionDto)position; // Position is found;
        }

        public PositionDto? Create(NewPositionDto newPositionDto)
        {
            var position = _positionRepository.Create(newPositionDto);
            if (position is null)
            {
                return null; // Position is null or not found;
            }

            return (PositionDto)position; // Position is found;
        }

        public int Update(PositionDto positionDto)
        {
            var position = _positionRepository.GetByGuid(positionDto.Guid);
            if (position is null)
            {
                return -1; // Position is null or not found;
            }

            Position toUpdate = positionDto;
            toUpdate.CreatedDate = position.CreatedDate;
            var result = _positionRepository.Update(toUpdate);

            return result ? 1 // Position is updated;
                : 0; // Position failed to update;
        }

        public int Delete(Guid guid)
        {
            var position = _positionRepository.GetByGuid(guid);
            if (position is null)
            {
                return -1; // Position is null or not found;
            }

            var result = _positionRepository.Delete(position);

            return result ? 1 // Position is deleted;
                : 0; // Position failed to delete;
        }
    }
}
