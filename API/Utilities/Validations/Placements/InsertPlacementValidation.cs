using API.DTOs.Placements;
using FluentValidation;

namespace API.Utilities.Validations.Placements
{
    public class InsertPlacementValidation : AbstractValidator<NewPlacementDto>
    {
        public InsertPlacementValidation()
        {
            RuleFor(Placement=>Placement.StartDate).NotEmpty().WithMessage("Start Date Required");
            RuleFor(Placement=>Placement.EndDate)
                .NotEmpty().WithMessage("End Date Required")
                .GreaterThan(Placement=>Placement.StartDate.AddDays(30));
            RuleFor(Placement => Placement.EmployeeGuid).NotEmpty().WithMessage("Employee Guid Required");
            RuleFor(Placement => Placement.ClientGuid).NotEmpty().WithMessage("Client Guid Required");
        }
    }
}
