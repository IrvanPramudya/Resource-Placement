using API.Contracts;
using API.DTOs.AccountRoles;
using FluentValidation;

namespace API.Utilities.Validations.AccountRoles
{
    public class InsertAccountRoleValidation:AbstractValidator<NewAccountRoleDto>
    {
        public InsertAccountRoleValidation(IAccountRoleRepository repository)
        {
            RuleFor(accounrole => accounrole.AccountGuid)
                .NotEmpty().WithMessage("Account Guid can not be Null");
            RuleFor(accounrole => accounrole.RoleGuid)
                .NotEmpty().WithMessage("Role Guid can not be Null");
        }
    }
}
