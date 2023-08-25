using API.Models;

namespace API.DTOs.Clients
{
    public class ClientDto
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsAvailable { get; set; }

        public static implicit operator Client(ClientDto client)
        {
            return new Client
            {
                Guid = client.Guid,
                Name = client.Name,
                Email = client.Email,
                IsAvailable = client.IsAvailable,
                ModifiedDate = DateTime.Now
            };
        }
        public static explicit operator ClientDto(Client client)
        {
            return new ClientDto
            {
                Guid = client.Guid,
                Name = client.Name,
                Email = client.Email,
                IsAvailable = client.IsAvailable,
            };
        }
    }
}
