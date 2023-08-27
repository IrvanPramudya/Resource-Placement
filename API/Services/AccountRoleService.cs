using API.Contracts;
using API.DTOs.AccountRoles;
using API.DTOs.Roles;
using API.Models;
using Microsoft.OpenApi.Any;

namespace API.Services
{
    public class AccountRoleService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IAccountRoleRepository _accountRoleRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IRoleRepository _roleRepository;

        public AccountRoleService(IAccountRoleRepository accountRoleRepository, IRoleRepository roleRepository, IAccountRepository accountRepository, IEmployeeRepository employeeRepository)
        {
            _accountRoleRepository = accountRoleRepository;
            _roleRepository = roleRepository;
            _accountRepository = accountRepository;
            _employeeRepository = employeeRepository;
        }
        public IEnumerable<GetAccountRolewithFullname>? GetAccountRolewithFullname() 
        {
            var merge = from employee in _employeeRepository.GetAll()
                        join account in _accountRepository.GetAll() on employee.Guid equals account.Guid
                        join accountrole in _accountRoleRepository.GetAll() on account.Guid equals accountrole.AccountGuid
                        join role in _roleRepository.GetAll() on accountrole.RoleGuid equals role.Guid
                        select new GetAccountRolewithFullname
                        {
                            Guid = accountrole.Guid,
                            AccountGuid = account.Guid,
                            RoleGuid = role.Guid,
                            FullName = employee.FirstName+ " " + employee.LastName,
                            RoleName = role.Name
                        };
            if(!merge.Any())
            {
                return null;
            }
            return merge;
        }
        public IEnumerable<GetEmployeehasAccount>? GetEmployeehasAccount()
        {
            var merge = from employee in _employeeRepository.GetAll()
                        join account in _accountRepository.GetAll() on employee.Guid equals account.Guid
                        join accountrole in _accountRoleRepository.GetAll() on account.Guid equals accountrole?.AccountGuid into accountrolegroup
                        from accountrole in accountrolegroup.DefaultIfEmpty()
                        select new GetEmployeehasAccount
                        {
                            AccountGuidAccountRole = accountrole != null?accountrole.AccountGuid:null,
                            RoleGuidAccountRole =  accountrole != null?accountrole.RoleGuid:null,
                            EmployeeGuid = employee.Guid,
                            AccountGuid = account.Guid,
                            FullName = employee.FirstName + " " + employee.LastName,
                        };
            if (!merge.Any())
            {
                return null;
            }
            return merge.Where(ar=>ar.RoleGuidAccountRole == null);
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
