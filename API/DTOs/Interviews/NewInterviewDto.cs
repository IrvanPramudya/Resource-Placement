using API.Models;

namespace API.DTOs.Interviews
{
    public class NewInterviewDto
    {
        public Guid Guid { get; set; }
        public DateTime InterviewDate { get; set; }
        public Guid ClientGuid { get; set; }
        public string? Text { get; set; }

        public static implicit operator Interview(NewInterviewDto interview)
        {
            return new Interview
            {
                Guid = interview.Guid,
                InterviewDate = interview.InterviewDate,
                ClientGuid = interview.ClientGuid,
                Text = interview.Text,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
            };
        }
        public static explicit operator NewInterviewDto(Interview interview)
        {
            return new NewInterviewDto
            {
                Guid = interview.Guid,
                InterviewDate = interview.InterviewDate,
                ClientGuid = interview.ClientGuid,
                Text = interview.Text,
            };
        }
    }
}
