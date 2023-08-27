using API.DTOs.Positions;
using API.Models;

namespace API.Contracts
{
    public interface IPositionRepository : IGeneralRepository<Position>
    {
        public Position? GetByClientGuid(Guid guid);
    }
}
