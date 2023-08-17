using API.Contracts;
using API.DTOs.Positions;
using API.Models;
using API.Repositories;

namespace API.Services
{
    public class PositionService
    {
        private readonly IPositionRepository _positionRepository;

        public PositionService(IPositionRepository positionRepository)
        {
            _positionRepository = positionRepository;
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
