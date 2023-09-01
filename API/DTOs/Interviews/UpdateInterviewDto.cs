using API.Models;
using API.Utilities.Enums;

namespace API.DTOs.Interviews
{
    public class UpdateInterviewDto
    {
        public Guid Guid { get; set; }
        public string? Comment { get; set; }
        public bool? IsAccepted { get; set; }
        public InterviewLevel Status { get; set; }

        public static implicit operator Interview(UpdateInterviewDto interview)
        {
            return new Interview
            {
                Guid = interview.Guid,
                IsAccepted = interview.IsAccepted,
                Status = interview.Status,
                ModifiedDate = DateTime.Now,
            };
        }
        public static explicit operator UpdateInterviewDto(Interview interview)
        {
            return new UpdateInterviewDto
            {
                Guid = interview.Guid,
                IsAccepted = interview.IsAccepted,
                Status = interview.Status,
            };
        }
    }
}
