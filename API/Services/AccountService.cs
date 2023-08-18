using API.Contracts;
using API.DTOs.Accounts;
using API.Models;
using API.Repositories;
using API.Utilities.Handlers;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace API.Services
{
    public class AccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountRoleRepository _accountRoleRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ITokenHandler _tokenHandler;

        public AccountService(IAccountRepository accountRepository, IAccountRoleRepository accountRoleRepository, IEmployeeRepository employeeRepository, ITokenHandler tokenHandler)
        {
            _accountRepository = accountRepository;
            _accountRoleRepository = accountRoleRepository;
            _employeeRepository = employeeRepository;
            _tokenHandler = tokenHandler;
        }

        public string Login(LoginDto loginDto)
        {
            var getEmployee = _employeeRepository.GetByEmail(loginDto.Email);


            if (getEmployee is null)
            {
                return "-1"; // Employee not found
            }

            var getAccount = _accountRepository.GetByGuid(getEmployee.Guid);

            if (!HashingHandler.ValidateHash(loginDto.Password, getAccount.Password))
            {
                return "-1"; // Login gagal
            }

            var getRoles = _accountRoleRepository.GetRoleNamesByAccountGuid(getEmployee.Guid);

            var claims = new List<Claim>
            {
                new Claim("Guid", getEmployee.Guid.ToString()),
                new Claim("Fullname", $"{getEmployee.FirstName}{getEmployee.LastName}"),
                new Claim("Email", getEmployee.Email)
            };

            foreach (var role in getRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var generatedToken = _tokenHandler.GenerateToken(claims);
            if (generatedToken is null)
            {
                return "-2";
            }
            return generatedToken;

        }

        public IEnumerable<AccountDto> GetAll()
        {
            var accounts = _accountRepository.GetAll();
            if (!accounts.Any())
            {
                return Enumerable.Empty<AccountDto>(); // Account is null or not found;
            }

            var accountDtos = new List<AccountDto>();
            foreach (var account in accounts)
            {
                accountDtos.Add((AccountDto)account);
            }

            return accountDtos; // Account is found;
        }

        public AccountDto? GetByGuid(Guid guid)
        {
            var account = _accountRepository.GetByGuid(guid);
            if (account is null)
            {
                return null; // Account is null or not found;
            }

            return (AccountDto)account; // Account is found;
        }

        public AccountDto? Create(NewAccountDto newAccountDto)
        {
            var account = _accountRepository.Create(newAccountDto);
            if (account is null)
            {
                return null; // Account is null or not found;
            }

            return (AccountDto)account; // Account is found;
        }

        public int Update(AccountDto accountDto)
        {
            var account = _accountRepository.GetByGuid(accountDto.Guid);
            if (account is null)
            {
                return -1; // Account is null or not found;
            }

            Account toUpdate = accountDto;
            toUpdate.CreatedDate = account.CreatedDate;
            var result = _accountRepository.Update(toUpdate);

            return result ? 1 // Account is updated;
                : 0; // Account failed to update;
        }

        public int Delete(Guid guid)
        {
            var account = _accountRepository.GetByGuid(guid);
            if (account is null)
            {
                return -1; // Account is null or not found;
            }

            var result = _accountRepository.Delete(account);

            return result ? 1 // Account is deleted;
                : 0; // Account failed to delete;
        }
    }
}
