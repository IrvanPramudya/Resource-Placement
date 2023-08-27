﻿namespace API.DTOs.Positions
{
    public class GetClientName
    {
        public Guid Guid { get; set; }
        public Guid ClientGuid { get; set; }
        public string? ClientName { get; set; }
        public string? PositionName { get; set; }
        public int Capacity { get; set; }
    }
}
