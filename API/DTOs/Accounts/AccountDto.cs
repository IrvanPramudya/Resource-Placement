using API.Models;

namespace API.DTOs.Accounts
{
    public class AccountDto
    {
        public Guid Guid { get; set; }
        public string Password { get; set; }
        public int? OTP { get; set; }
        public bool IsUsed { get; set; }
        public DateTime? ExpiredTime { get; set; }

        public static implicit operator Account(AccountDto dto)
        {
            return new Account
            {
                Guid = dto.Guid,
                OTP = dto.OTP,
                Password = dto.Password,
                IsUsed = dto.IsUsed,
                ExpiredTime = dto.ExpiredTime,
                ModifiedDate = DateTime.Now
            };
        }
        public static explicit operator AccountDto(Account account)
        {
            return new AccountDto
            {
                Guid = account.Guid,
                OTP = account.OTP,
                Password = account.Password,
                IsUsed = account.IsUsed,
                ExpiredTime = account.ExpiredTime
            };
        }
    }
}
