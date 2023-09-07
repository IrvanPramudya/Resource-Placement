using API.Models;

namespace API.Contracts
{
    public interface IHistoryRepository : IGeneralRepository<History>
    {
        IEnumerable<History>? GetHistoryByEmployeeGuid(Guid guid);
    }
}
