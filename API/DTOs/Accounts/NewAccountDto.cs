using API.Models;
using API.Utilities.Handlers;

namespace API.DTOs.Accounts
{
    public class NewAccountDto
    {
        public Guid Guid { get; set; }
        public int OTP { get; set; }
        public string Password { get; set; }
        public bool IsUsed { get; set; }
        /*public DateTime ExpiredTime { get; set; }*/

        public static implicit operator Account(NewAccountDto dto)
        {
            return new Account
            {
                Guid = dto.Guid,
                OTP = dto.OTP,
                Password = HashingHandler.GenerateHash(dto.Password),
                IsUsed = dto.IsUsed,
                /*ExpiredTime = dto.ExpiredTime,*/
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
            };
        }
        public static explicit operator NewAccountDto(Account account)
        {
            return new NewAccountDto
            {
                Guid = account.Guid,
                OTP = account.OTP,
                Password = account.Password,
                IsUsed = account.IsUsed,
                /*ExpiredTime = account.ExpiredTime,*/
            };
        }
    }
}
