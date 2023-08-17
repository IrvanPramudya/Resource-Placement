using API.Contracts;
using API.Models;

namespace API.Services
{
    public class ClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public IEnumerable<ClientDto> GetAll()
        {
            var clients = _clientRepository.GetAll();
            if (!clients.Any())
            {
                return Enumerable.Empty<ClientDto>(); // Client is null or not found;
            }

            var clientDtos = new List<ClientDto>();
            foreach (var client in clients)
            {
                clientDtos.Add((ClientDto)client);
            }

            return clientDtos; // Client is found;
        }

        public ClientDto? GetByGuid(Guid guid)
        {
            var client = _clientRepository.GetByGuid(guid);
            if (client is null)
            {
                return null; // Client is null or not found;
            }

            return (ClientDto)client; // Client is found;
        }

        public ClientDto? Create(NewClientDto newClientDto)
        {
            var client = _clientRepository.Create(newClientDto);
            if (client is null)
            {
                return null; // Client is null or not found;
            }

            return (ClientDto)client; // Client is found;
        }

        public int Update(ClientDto clientDto)
        {
            var client = _clientRepository.GetByGuid(clientDto.Guid);
            if (client is null)
            {
                return -1; // Client is null or not found;
            }

            Client toUpdate = clientDto;
            toUpdate.CreatedDate = client.CreatedDate;
            var result = _clientRepository.Update(toUpdate);

            return result ? 1 // Client is updated;
                : 0; // Client failed to update;
        }

        public int Delete(Guid guid)
        {
            var client = _clientRepository.GetByGuid(guid);
            if (client is null)
            {
                return -1; // Client is null or not found;
            }

            var result = _clientRepository.Delete(client);

            return result ? 1 // Client is deleted;
                : 0; // Client failed to delete;
        }
    }
}
