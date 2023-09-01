using API.Models;

namespace API.DTOs.Clients
{
    public class UpdateClientDto
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public static implicit operator Client(UpdateClientDto client)
        {
            return new Client
            {
                Guid = client.Guid,
                Name = client.Name,
                Email = client.Email,
                ModifiedDate = DateTime.Now
            };
        }
        public static explicit operator UpdateClientDto(Client client)
        {
            return new UpdateClientDto
            {
                Guid = client.Guid,
                Name = client.Name,
                Email = client.Email,
            };
        }
    }
}
