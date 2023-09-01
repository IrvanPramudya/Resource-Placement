using API.Models;
using API.Utilities.Handlers;

namespace API.DTOs.Accounts
{
    public class NewAccountDto
    {
        public Guid Guid { get; set; }
        public string Password { get; set; }

        public static implicit operator Account(NewAccountDto dto)
        {
            return new Account
            {
                Guid = dto.Guid,
                Password = HashingHandler.GenerateHash(dto.Password),
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
            };
        }
        public static explicit operator NewAccountDto(Account account)
        {
            return new NewAccountDto
            {
                Guid = account.Guid,
                Password = account.Password,
            };
        }
    }
}
