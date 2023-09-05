using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories
{
    public class HistoryRepository : GeneralRepository<History>, IHistoryRepository
    {
        public HistoryRepository(PlacementDbContext context) : base(context) { }
        public IEnumerable<History>? GetHistoryByEmployeeGuid(Guid guid)
        {
            var result = _context.Set<History>().Where(his => his.EmployeeGuid == guid).ToList();
            return result;
        }

    }
}
