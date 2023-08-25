using API.Models;

namespace API.DTOs.Clients
{
    public class NewClientDto
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public static implicit operator Client(NewClientDto client)
        {
            return new Client
            {
                Guid = new Guid(),
                Name = client.Name,
                Email = client.Email,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
        }
        public static explicit operator NewClientDto(Client client)
        {
            return new NewClientDto
            {
                Name = client.Name,
                Email = client.Email,
            };
        }
    }
}
