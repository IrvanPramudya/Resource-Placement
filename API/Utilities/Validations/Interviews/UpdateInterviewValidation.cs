using API.DTOs.Interviews;
using FluentValidation;

namespace API.Utilities.Validations.Interviews
{
    public class UpdateInterviewValidation : AbstractValidator<InterviewDto>
    {
        public UpdateInterviewValidation()
        {
            RuleFor(Interview => Interview.InterviewDate).NotEmpty().WithMessage("Interview Date Required");
            RuleFor(Interview => Interview.ClientGuid).NotEmpty().WithMessage("Client Guid Required");
        }
    }
}
