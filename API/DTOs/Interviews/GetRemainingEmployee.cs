using API.Utilities.Enums;

namespace API.DTOs.Interviews
{
    public class GetRemainingEmployee
    {
        public Guid Guid { get; set; }
        public string FullName { get; set; }
        public string? ClientName { get; set; }
        public string? PositionName { get; set; }
        public DateTime? InterviewDate { get; set; }
        public DateTime? StartDate { get; set; }
        public Guid? ClientGuid { get; set; }
        public Guid? PlacementGuid { get; set; }
        public Guid? PositionGuid { get; set; }
        public InterviewLevel? Status { get; set; }
        
    }
}
