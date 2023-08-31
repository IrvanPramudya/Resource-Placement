using API.Contracts;
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
        private readonly IAccountRoleRepository _accountRoleRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IGradeRepository _gradeRepository;
        private readonly IInterviewRepository _interviewRepository;
        private readonly IPlacementRepository _placementRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IPositionRepository _positionRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly ITokenHandler _tokenHandler;
        private readonly PlacementDbContext _dbContext;
        private readonly IEmailHandler _emailHandler;

        public AccountService(IAccountRepository accountRepository, IAccountRoleRepository accountRoleRepository, IEmployeeRepository employeeRepository, ITokenHandler tokenHandler, PlacementDbContext dbContext, IEmailHandler emailHandler, IGradeRepository gradeRepository, IInterviewRepository interviewRepository, IPlacementRepository placementRepository, IClientRepository clientRepository, IPositionRepository positionRepository, IRoleRepository roleRepository)
        {
            _accountRepository = accountRepository;
            _accountRoleRepository = accountRoleRepository;
            _employeeRepository = employeeRepository;
            _tokenHandler = tokenHandler;
            _dbContext = dbContext;
            _emailHandler = emailHandler;
            _gradeRepository = gradeRepository;
            _interviewRepository = interviewRepository;
            _placementRepository = placementRepository;
            _clientRepository = clientRepository;
            _positionRepository = positionRepository;
            _roleRepository = roleRepository;
        }
        public GetAccountDetail? DetailAccount(Guid id)
        {
            var merge = from employee in _employeeRepository.GetAll()
                        join grade in _gradeRepository.GetAll() on employee.Guid equals grade.Guid into GrdGrp
                        from grade in GrdGrp.DefaultIfEmpty()
                        join account in _accountRepository.GetAll() on employee.Guid equals account.Guid
                        join accountrole in _accountRoleRepository.GetEmployeewithEmployeeRole() on account.Guid equals accountrole.AccountGuid
                        join placement in _placementRepository.GetAll() on employee.Guid equals placement.Guid into PlcGrp
                        from placement in PlcGrp.DefaultIfEmpty()
                        join interview in _interviewRepository.GetAll() on employee.Guid equals interview.Guid into InterGrp
                        from interview in InterGrp.DefaultIfEmpty()
                        join client in _clientRepository.GetAll() on (interview != null ? interview.ClientGuid : placement.ClientGuid) equals client.Guid into CliGrp
                        from client in CliGrp.DefaultIfEmpty()
                        join position in _positionRepository.GetAll() on client.Guid equals position.ClientGuid into PosGrp
                        from position in PosGrp.DefaultIfEmpty()
                        join role in _roleRepository.GetAll() on accountrole.RoleGuid equals role.Guid into RoleGrp
                        from role in RoleGrp.DefaultIfEmpty()
                        where employee.Guid == id
                        select new GetAccountDetail
                        {
                            FullName = employee.FirstName + " " + employee.LastName,
                            Email = employee.Email,
                            Gender = employee.Gender,
                            Status = employee.Status,
                            Skill = employee.Skill,
                            NIK = employee.NIK,
                            PhoneNumber = employee.PhoneNumber,
                            RoleName = role != null ? role.Name : null,
                            ClientInterviewName = interview != null ? client.Name : null,
                            InterviewDate = interview != null ? interview.InterviewDate : null,
                            PositionInterviewName = interview != null ? position.Name : null,
                            GradeName = grade != null ? grade.Name : null,
                            Salary = grade != null ? grade.Salary : null,
                            PositionPlacementName = placement != null ? position.Name : null,
                            ClientPlacementName = placement != null ? client.Name : null,
                            Contract = placement != null ? (placement.EndDate - placement.StartDate).Days : 0
                        };
            if(!merge.Any())
            {
                return null;
            }
            return merge.FirstOrDefault();
        }
        public IEnumerable<GetAccountwithNameAndRoles>? GetAccountwithNamesAndRolesOuterJoin()
        {
            var merge = from employee in _employeeRepository.GetAll()
                        join account in _accountRepository.GetAll() on employee.Guid equals account.Guid into accountGroup
                        from account in accountGroup.DefaultIfEmpty()
                        join accountRole in _accountRoleRepository.GetAll() on account.Guid equals accountRole.AccountGuid into accountRoleGroup
                        from accountRole in accountRoleGroup.DefaultIfEmpty()
                        select new GetAccountwithNameAndRoles
                        {
                            Guid = employee.Guid,
                            FullName = employee.FirstName + " " + employee.LastName,
                            IsUsed = account != null ? account.IsUsed : false,
                            OTP = account != null ? account.OTP : null,
                            Password = account != null ? account.Password : null,
                            RoleGuid = accountRole != null ? accountRole.RoleGuid : null,
                        };
            return merge;
        }

        public IEnumerable<GetAccountwithName>? GetAccountwithNames()
        {
            var merge = from employee in _employeeRepository.GetAll()
                        join account in _accountRepository.GetAll() on employee.Guid equals account.Guid
                        select new GetAccountwithName
                        {
                            Guid = employee.Guid,
                            FullName = employee.FirstName + " " + employee.LastName,
                            IsUsed = account.IsUsed,
                            OTP = account.OTP,
                            Password = account.Password
                        };
            return merge;
        }
        public IEnumerable<GetAccountwithName>? GetAccountwithNamesOuterJoin()
        {
            var merge = from employee in _employeeRepository.GetAll()
                        join account in _accountRepository.GetAll() on employee.Guid equals account.Guid into accountGroup
                        from account in accountGroup.DefaultIfEmpty()
                        select new GetAccountwithName
                        {
                            Guid = employee.Guid,
                            FullName = employee.FirstName + " " + employee.LastName,
                            IsUsed = account != null ? account.IsUsed : false,
                            OTP = account != null ? account.OTP : null,
                            Password = account != null ? account.Password : null
                        };
            return merge.Where(a=>a.Password == null);
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
            var getInterview = _interviewRepository.GetByGuid(getEmployee.Guid);

            var claims = new List<Claim>
            {
                new Claim("Guid", getEmployee.Guid.ToString()),
                new Claim("Fullname", $"{getEmployee.FirstName} {getEmployee.LastName}"),
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

        public int Register(RegisterDto register)
        {
            if (!_employeeRepository.IsNotExist(register.Email)
                || !_employeeRepository.IsNotExist(register.PhoneNumber))
            {
                return 0;
            }
            using var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                var NewNik = GenerateHandler.LastNik(_employeeRepository.GetLastNik());
                var employee = _employeeRepository.Create(new Employee
                {
                    Guid = new Guid(),
                    NIK = NewNik,
                    FirstName = register.FirstName,
                    LastName = register.LastName,
                    Email = register.Email,
                    PhoneNumber = register.PhoneNumber,
                    Gender = register.Gender,
                    Status = 0,
                    Skill = register.Skill,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                });
                var account = _accountRepository.Create(new Account
                {
                    Guid = employee.Guid,
                    Password = HashingHandler.GenerateHash(register.Password),
                    OTP = null,
                    IsUsed = true,
                    ExpiredTime = null,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                });
                var accountrole = _accountRoleRepository.GetAll();
                if (!accountrole.Any())// Jika Account Role Kosong maka Inputan Register pertama akan menjadi Admin
                {
                    var accountroleadmin = _accountRoleRepository.Create(new NewAccountRoleDto
                    {
                        AccountGuid = account.Guid,
                        RoleGuid = Guid.Parse("5FB9ADC0-7D08-45D4-CD66-08DB9C7A678F")
                    });
                    transaction.Commit();
                    return 1;
                }
                var accountroleemployee = _accountRoleRepository.Create(new NewAccountRoleDto
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

        public int ForgotPassword(ForgotPasswordDto forgotPasswordDto)
        {
            var otp = new Random().Next(111111, 999999);
            var getAccountDetail = (from e in _employeeRepository.GetAll()
                                    join a in _accountRepository.GetAll() on e.Guid equals a.Guid
                                    where e.Email == forgotPasswordDto.Email
                                    select a).FirstOrDefault();

            if (getAccountDetail is null)
            {
                return 0; // no email found
            }

            _accountRepository.Clear();

            var hashedPassword = HashingHandler.GenerateHash(getAccountDetail.Password);
            var isUpdated = _accountRepository.Update(new Account


            {
                Guid = getAccountDetail.Guid,
                Password = hashedPassword,
                //ExpiredTime = DateTime.Now.AddMinutes(5),
                OTP = otp,
                IsUsed = false,
                CreatedDate = getAccountDetail.CreatedDate,
                ModifiedDate = getAccountDetail.ModifiedDate
            });

            if (!isUpdated)
            {
                return -1; // error update
            }

            _emailHandler.SendEmail(forgotPasswordDto.Email, "OTP", $"Your OTP id : {otp}");
            return 1;
        }

        public int ChangePassword(ChangePasswordDto changePasswordDto)
        {
            var getAccount = (from e in _employeeRepository.GetAll()
                              join a in _accountRepository.GetAll() on e.Guid equals a.Guid
                              where e.Email == changePasswordDto.Email
                              select a).FirstOrDefault();

            if (getAccount is null)
            {
                return 0;
            }
            var hashedPassword = HashingHandler.GenerateHash(changePasswordDto.Password);
            var account = new Account
            {
                Guid = getAccount.Guid,
                IsUsed = true,
                ModifiedDate = DateTime.Now,
                CreatedDate = getAccount.CreatedDate,
                OTP = getAccount.OTP,
                ExpiredTime = getAccount.ExpiredTime,
                Password = hashedPassword,
            };

            if (getAccount.OTP != changePasswordDto.OTP)
            {
                return -1;
            }

            if (getAccount.IsUsed == true)
            {
                return -2;
            }

            if (getAccount.ExpiredTime < DateTime.Now)
            {
                return -3;  //OTP expired
            }

            _accountRepository.Clear();

            var isUpdated = _accountRepository.Update(account);
            if (!isUpdated)
            {
                return -4; //Account not Update
            }

            return 3;
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
            Account accounttoCreate = newAccountDto;
            accounttoCreate.OTP = null;
            accounttoCreate.ExpiredTime = null;
            accounttoCreate.IsUsed = true;
            var account = _accountRepository.Create(accounttoCreate);
            if (account is null)
            {
                return null; // Account is null or not found;
            }

            return (AccountDto)account; // Account is found;
        }

        public int Update(NewAccountDto accountDto)
        {
            var account = _accountRepository.GetByGuid(accountDto.Guid);
            if (account is null)
            {
                return -1; // Account is null or not found;
            }

            Account toUpdate = accountDto;
            toUpdate.OTP = account.OTP;
            toUpdate.ExpiredTime = account.ExpiredTime;
            toUpdate.IsUsed = account.IsUsed;
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
