namespace API.DTOs.Histories
{
    public class CountStatusHistories
    {
        public int CountWaiting { get; set; }
        public int CountAcceptedEmployee { get; set; }
        public int CountAcceptedClient { get; set; }
        public int CountRejectedEmployee { get; set; }
        public int CountRejectedClient { get; set; }
    }
}
