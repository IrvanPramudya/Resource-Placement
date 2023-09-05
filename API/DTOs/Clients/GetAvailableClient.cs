namespace API.DTOs.Clients
{
    public class GetAvailableClient
    {
        public Guid ClientGuid { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string? PositionName { get; set; }
        public int Capacity { get; set; }
        public bool IsAvailable { get; set; }
    }
}
