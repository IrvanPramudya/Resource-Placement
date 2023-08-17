using API.Models;

namespace API.DTOs.Interviews
{
    public class InterviewDto
    {
        public Guid Guid { get; set; }
        public DateTime InterviewDate { get; set; }
        public Guid ClientGuid { get; set; }
        public string? Text { get; set; }

        public static implicit operator Interview(InterviewDto interview)
        {
            return new Interview
            {
                Guid = interview.Guid,
                InterviewDate = interview.InterviewDate,
                ClientGuid = interview.ClientGuid,
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
                Text = interview.Text,
            };
        }
    }
}
