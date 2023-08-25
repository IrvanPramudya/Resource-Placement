namespace API.DTOs.Accounts
{
    public class GetAccountwithName
    {
        public Guid Guid { get; set; }
        public string FullName { get; set; }    
        public string Password { get; set; }    
        public int? OTP { get; set; }    
        public bool IsUsed { get; set; }    
    }
}
