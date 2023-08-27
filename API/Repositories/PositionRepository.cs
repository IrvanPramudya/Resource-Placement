using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories
{
    public class PositionRepository : GeneralRepository<Position>, IPositionRepository
    {
        public PositionRepository(PlacementDbContext context) : base(context) { }

        public Position? GetByClientGuid(Guid guid)
        {
            return _context.Set<Position>().SingleOrDefault(client=>client.ClientGuid.Equals(guid));
        }
    }
}
