namespace API.DTOs.Positions
{
    public class GetClientName
    {
        public Guid Guid { get; set; }
        public string? ClientName { get; set; }
        public string? PositionName { get; set; }
    }
}
