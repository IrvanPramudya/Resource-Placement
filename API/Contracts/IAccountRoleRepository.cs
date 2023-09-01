using API.Models;

namespace API.Contracts
{
    public interface IAccountRoleRepository : IGeneralRepository<AccountRole>
    {
        IEnumerable<string>? GetRoleNamesByAccountGuid(Guid guid);
        public IEnumerable<AccountRole>? GetEmployeewithEmployeeRole();
        /*public bool IsNotExist(Guid RoleGuid);*/
    }
}
