using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories
{
    public class PositionRepository : GeneralRepository<Position>, IPositionRepository
    {
        public PositionRepository(PlacementDbContext context) : base(context) { }

    }
}
