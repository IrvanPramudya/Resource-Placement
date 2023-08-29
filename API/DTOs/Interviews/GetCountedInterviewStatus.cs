using API.Utilities.Enums;

namespace API.DTOs.Interviews
{
    public class GetCountedInterviewStatus
    {
        public int? CountStatusWaiting { get; set; }
        public int? CountStatusAcceptedbyEmployee { get; set; }
        public int? CountStatusRejectedbyEmployee { get; set; }
        public int? CountStatusAcceptedbyClient { get; set; }
        public int? CountStatusRejectedbyClient { get; set; }
    }
}
