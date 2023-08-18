namespace API.DTOs.Clients
{
    public class GetAvailableClient
    {
        public string Name { get; set; }
        public bool IsAvailable { get; set; }
        public int Capacity { get; set; }
    }
}
