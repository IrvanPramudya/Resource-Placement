using API.Contracts;
using API.DTOs.Employees;
using FluentValidation;

namespace API.Utilities.Validations.Employee
{
    public class InsertEmployeeValidator : AbstractValidator<NewEmployeeDto>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public InsertEmployeeValidator(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            RuleFor(employee => employee.FirstName)
                .NotEmpty().WithMessage("First Name is Required");
            RuleFor(employee => employee.Gender)
                .IsInEnum().WithMessage("Gender is not Identify");
            RuleFor(employee => employee.Status)
                .IsInEnum().WithMessage("Status is not Identify");
            RuleFor(employee => employee.Email)
                .NotEmpty().WithMessage("Email is Required")
                .EmailAddress().WithMessage("Wrong Email")
                .Must(IsDuplicateValue).WithMessage("Email is Already Exist");
            RuleFor(employee => employee.PhoneNumber)
                .NotEmpty().WithMessage("Phone Number is Required")
                .MaximumLength(20).WithMessage("Maximum 20 Character")
                .Matches(@"^\+[0-9]")
                .Must(IsDuplicateValue).WithMessage("Phone is Already Exist");
        }

        private bool IsDuplicateValue(string arg)
        {
            return _employeeRepository.IsNotExist(arg);
        }
    }
}
