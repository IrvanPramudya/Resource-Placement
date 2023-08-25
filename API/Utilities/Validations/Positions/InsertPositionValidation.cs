using API.DTOs.Positions;
using FluentValidation;

namespace API.Utilities.Validations.Positions
{
    public class InsertPositionValidation : AbstractValidator<NewPositionDto>
    {
        public InsertPositionValidation()
        {
            RuleFor(Position=>Position.Guid).NotEmpty().WithMessage("Guid Required");
            RuleFor(Position=>Position.Name).NotEmpty().WithMessage("Name of Position Required");
            RuleFor(client => client.Capacity)
                .NotEmpty().WithMessage("Capacity Cannot be Null");
        }
    }
}
