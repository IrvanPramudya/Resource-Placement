using API.Contracts;
using API.DTOs.AccountRoles;
using API.Models;

namespace API.Services
{
    public class AccountRoleService
    {
        private readonly IAccountRoleRepository _accountRoleRepository;
        private readonly IRoleRepository _roleRepository;

        public AccountRoleService(IAccountRoleRepository accountRoleRepository, IRoleRepository roleRepository)
        {
            _accountRoleRepository = accountRoleRepository;
            _roleRepository = roleRepository;
        }
        public IEnumerable<GetCountedAllRole> CountAllRole()
        {
            var accountrole = _accountRoleRepository.GetAll();
            var role = _roleRepository.GetAll();
            var listrole = new List<GetCountedAllRole>();
            foreach(var item in role)
            {
                var newrole = new GetCountedAllRole
                {
                    RoleName = item.Name,
                    CountRole = 0
                };
                foreach(var item2 in accountrole)
                {
                    if(item.Guid == item2.RoleGuid)
                    {
                        newrole.CountRole++;
                    }
                }
                listrole.Add(newrole);
            }
            return listrole;
        }
        public IEnumerable<AccountRoleDto> GetAll()
        {
            var accountRoles = _accountRoleRepository.GetAll();
            if (!accountRoles.Any())
            {
                return Enumerable.Empty<AccountRoleDto>(); // AccountRole is null or not found;
            }

            var accountRoleDtos = new List<AccountRoleDto>();
            foreach (var accountRole in accountRoles)
            {
                accountRoleDtos.Add((AccountRoleDto)accountRole);
            }

            return accountRoleDtos; // AccountRole is found;
        }

        public AccountRoleDto? GetByGuid(Guid guid)
        {
            var accountRole = _accountRoleRepository.GetByGuid(guid);
            if (accountRole is null)
            {
                return null; // AccountRole is null or not found;
            }

            return (AccountRoleDto)accountRole; // AccountRole is found;
        }

        public AccountRoleDto? Create(NewAccountRoleDto newAccountRoleDto)
        {
            var accountRole = _accountRoleRepository.Create(newAccountRoleDto);
            if (accountRole is null)
            {
                return null; // AccountRole is null or not found;
            }

            return (AccountRoleDto)accountRole; // AccountRole is found;
        }

        public int Update(AccountRoleDto accountRoleDto)
        {
            var accountRole = _accountRoleRepository.GetByGuid(accountRoleDto.Guid);
            if (accountRole is null)
            {
                return -1; // AccountRole is null or not found;
            }

            AccountRole toUpdate = accountRoleDto;
            toUpdate.CreatedDate = accountRole.CreatedDate;
            var result = _accountRoleRepository.Update(toUpdate);

            return result ? 1 // AccountRole is updated;
                : 0; // AccountRole failed to update;
        }

        public int Delete(Guid guid)
        {
            var accountRole = _accountRoleRepository.GetByGuid(guid);
            if (accountRole is null)
            {
                return -1; // AccountRole is null or not found;
            }

            var result = _accountRoleRepository.Delete(accountRole);

            return result ? 1 // AccountRole is deleted;
                : 0; // AccountRole failed to delete;
        }
    }
}
