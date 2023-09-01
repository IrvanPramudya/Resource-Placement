using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories
{
    public class PositionRepository : GeneralRepository<Position>, IPositionRepository
    {
        public PositionRepository(PlacementDbContext context) : base(context) { }

        public IEnumerable<Position>? GetByClientGuid(Guid guid)
        {
            return _context.Set<Position>().ToList().Where(client=>client.ClientGuid.Equals(guid));
        }
        public Position? GetOneByClientGuid(Guid guid)
        {
            return _context.Set<Position>().SingleOrDefault(client=>client.ClientGuid.Equals(guid));
        }
    }
}
