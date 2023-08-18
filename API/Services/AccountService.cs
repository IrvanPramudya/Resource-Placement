﻿using API.Contracts;
using API.Data;
using API.DTOs.AccountRoles;
using API.DTOs.Accounts;
using API.Models;
using API.Repositories;
using API.Utilities.Handlers;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace API.Services
{
    public class AccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IEmployeeRepository _employeerepository;
        private readonly IAccountRoleRepository _accountrolerepository;
        private readonly PlacementDbContext _dbContext;

        public AccountService(IAccountRepository accountRepository, IEmployeeRepository employeerepository, IAccountRoleRepository accountrolerepository, PlacementDbContext dbContext)
        {
            _accountRepository = accountRepository;
            _employeerepository = employeerepository;
            _accountrolerepository = accountrolerepository;
            _dbContext = dbContext;
        }
        public int register(RegisterDto register)
        {
            if (!_employeerepository.IsNotExist(register.Email)
                || !_employeerepository.IsNotExist(register.PhoneNumber))
            {
                return 0;
            }
            using var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                var NewNik = GenerateHandler.LastNik(_employeerepository.GetLastNik());
                var employee = _employeerepository.Create(new Employee
                {
                    Guid = new Guid(),
                    NIK = NewNik,
                    FirstName = register.FirstName,
                    LastName = register.LastName,
                    Email = register.Email,
                    PhoneNumber = register.PhoneNumber,
                    Gender = register.Gender,
                    Status = register.Status,
                    Skill = register.Skill,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                });
                var account = _accountRepository.Create(new Account
                {
                    Guid = employee.Guid,
                    Password = HashingHandler.GenerateHash(register.Password),
                    OTP = new Random().Next(111111, 999999),
                    IsUsed = true,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                });
                var accountrole = _accountrolerepository.GetAll();
                if (!accountrole.Any())// Jika Account Role Kosong maka Inputan Register pertama akan menjadi Admin
                {
                    var accountroleadmin = _accountrolerepository.Create(new NewAccountRoleDto
                    {
                        AccountGuid = account.Guid,
                        RoleGuid = Guid.Parse("5FB9ADC0-7D08-45D4-CD66-08DB9C7A678F")
                    });
                    transaction.Commit();
                    return 1;
                }
                var accountroleemployee = _accountrolerepository.Create(new NewAccountRoleDto
                {
                    AccountGuid = account.Guid,
                    RoleGuid = Guid.Parse("ae259a90-e2e8-442f-ce18-08db91a71ab9")
                });
                transaction.Commit();
                return 1;
            }
            catch
            {
                transaction.Rollback();
                return -1;
            }

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
