using API.Contracts;
using API.DTOs.Accounts;
using FluentValidation;

namespace API.Utilities.Validations.Accounts
{
    public class RegisterValidator : AbstractValidator<RegisterDto>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public RegisterValidator(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;

            RuleFor(l => l.FirstName)
                .NotEmpty();
            RuleFor(l => l.Skill)
                .NotEmpty();
            RuleFor(l => l.Gender)
                .NotNull()
                .IsInEnum();
            RuleFor(l => l.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Email is not valid")
                .Must(IsDuplicateValue).WithMessage("Email already exists"); ;
            RuleFor(l => l.PhoneNumber).NotEmpty().MaximumLength(20).Matches(@"^\+[0-9]")
                .Must(IsDuplicateValue).WithMessage("Phone Number already exists");
            RuleFor(l => l.Password)
                .NotEmpty().WithMessage("Password is required")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$");

        }

        private bool IsDuplicateValue(string value)
        {
            return _employeeRepository.IsNotExist(value);
        }
    }
}
