using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories
{
    public class PlacementRepository : GeneralRepository<Placement>, IPlacementRepository
    {
        public PlacementRepository(PlacementDbContext context) : base(context) { }

    }
}
