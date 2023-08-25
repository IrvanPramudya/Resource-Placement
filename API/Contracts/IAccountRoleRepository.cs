using API.Models;

namespace API.Contracts
{
    public interface IAccountRoleRepository : IGeneralRepository<AccountRole>
    {
        IEnumerable<string>? GetRoleNamesByAccountGuid(Guid guid);
        public bool IsNotExist(Guid RoleGuid);
    }
}
