using API.Contracts;
using API.DTOs.AccountRoles;
using API.Repositories;
using FluentValidation;

namespace API.Utilities.Validations.AccountRoles
{
    public class InsertAccountRoleValidation:AbstractValidator<NewAccountRoleDto>
    {
        private readonly IAccountRoleRepository _accountroleRepository;
        public InsertAccountRoleValidation(IAccountRoleRepository accountroleRepository)
        {
            _accountroleRepository = accountroleRepository;
            RuleFor(accounrole => accounrole.AccountGuid)
                .NotEmpty().WithMessage("Account Guid can not be Null");
            RuleFor(accounrole => accounrole.RoleGuid)
                .NotEmpty().WithMessage("Role Guid can not be Null")
                .Must(IsDuplicateValue).WithMessage("Cannot Get Same Role");
        }

        private bool IsDuplicateValue(Guid guid)
        {
            return _accountroleRepository.IsNotExist(guid);
        }
    }
}
