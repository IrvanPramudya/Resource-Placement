namespace API.DTOs.Interviews
{
    public class InformatifInterview
    {
        public Guid Guid { get; set; }
        public Guid ClientGuid { get; set; }
        public string FullName { get; set; }
        public string ClientName { get; set; }
        public string Text { get; set; }
        public DateTime InterviewDate { get; set; }

    }
}
