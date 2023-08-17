using API.DTOs.Interviews;
using FluentValidation;

namespace API.Utilities.Validations.Interviews
{
    public class InsertInterviewValidation : AbstractValidator<NewInterviewDto>
    {
        public InsertInterviewValidation()
        {
            RuleFor(Interview => Interview.InterviewDate).NotEmpty().WithMessage("Interview Date Required");
            RuleFor(Interview => Interview.ClientGuid).NotEmpty().WithMessage("Client Guid Required");
        }
    }
}
