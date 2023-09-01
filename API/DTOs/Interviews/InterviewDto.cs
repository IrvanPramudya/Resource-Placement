using API.Models;

namespace API.DTOs.Interviews
{
    public class InterviewDto
    {
        public Guid Guid { get; set; }
        public DateTime InterviewDate { get; set; }
        public Guid ClientGuid { get; set; }
        public Guid PositionGuid { get; set; }
        public string? Text { get; set; }
<<<<<<< Updated upstream
=======
        public string? Comment { get; set; }
        public bool? IsAccepted { get; set; }
        public InterviewLevel Status { get; set; }
>>>>>>> Stashed changes

        public static implicit operator Interview(InterviewDto interview)
        {
            return new Interview
            {
                Guid = interview.Guid,
                InterviewDate = interview.InterviewDate,
                ClientGuid = interview.ClientGuid,
                PositionGuid = interview.PositionGuid,
                Text = interview.Text,
                ModifiedDate = DateTime.Now,
            };
        }
        public static explicit operator InterviewDto(Interview interview)
        {
            return new InterviewDto
            {
                Guid = interview.Guid,
                InterviewDate = interview.InterviewDate,
                ClientGuid = interview.ClientGuid,
<<<<<<< Updated upstream
=======
                PositionGuid = interview.PositionGuid,
                IsAccepted = interview.IsAccepted,
                Status = interview.Status,
>>>>>>> Stashed changes
                Text = interview.Text,
            };
        }
    }
}
