using API.Models;

namespace API.Contracts
{
    public interface IPositionRepository : IGeneralRepository<Position>
    {
<<<<<<< Updated upstream
=======
        public IEnumerable<Position>? GetByClientGuid(Guid guid);
        public Position? GetOneByClientGuid(Guid guid);
>>>>>>> Stashed changes
    }
}
