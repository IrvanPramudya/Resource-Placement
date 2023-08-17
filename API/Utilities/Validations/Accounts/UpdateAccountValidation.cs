using API.Contracts;
using API.DTOs.Accounts;
using FluentValidation;

namespace API.Utilities.Validations.Accounts
{
    public class UpdateAccountValidation:AbstractValidator<AccountDto>
    {
        public UpdateAccountValidation()
        {
            RuleFor(account => account.Password)
                .NotEmpty().WithMessage("Password can not be Null")
                .Matches("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");
            RuleFor(account => account.OTP)
                .NotEmpty().WithMessage("OTP can not be Null");
        }
    }
}
