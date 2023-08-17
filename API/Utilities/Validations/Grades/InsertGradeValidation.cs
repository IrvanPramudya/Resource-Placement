using API.DTOs.Grades;
using FluentValidation;

namespace API.Utilities.Validations.Grades
{
    public class InsertGradeValidation : AbstractValidator<NewGradeDto>
    {
        public InsertGradeValidation()
        {
            RuleFor(Grade=>Grade.Name).NotEmpty().WithMessage("Name Required");
            RuleFor(Grade=>Grade.Salary).NotEmpty().WithMessage("Salary Required");
        }
    }
}
