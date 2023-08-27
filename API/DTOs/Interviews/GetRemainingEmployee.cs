﻿using API.Utilities.Enums;

namespace API.DTOs.Interviews
{
    public class GetRemainingEmployee
    {
        public Guid Guid { get; set; }
        public string FullName { get; set; }
        public DateTime? InterviewDate { get; set; }
        public DateTime? StartDate { get; set; }
        public Guid? ClientGuid { get; set; }
        public Guid? PlacementGuid { get; set; }
        public InterviewLevel? Status { get; set; }
        
    }
}
