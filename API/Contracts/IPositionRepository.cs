using API.DTOs.Positions;
using API.Models;

namespace API.Contracts
{
    public interface IPositionRepository : IGeneralRepository<Position>
    {
        public IEnumerable<Position>? GetByClientGuid(Guid guid);
        public Position? GetOneByClientGuid(Guid guid);
    }
}
