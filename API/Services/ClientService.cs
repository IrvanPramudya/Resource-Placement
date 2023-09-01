﻿using API.Contracts;
using API.DTOs.Clients;
using API.Models;

namespace API.Services
{
    public class ClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IPositionRepository _positionRepository;

        public ClientService(IClientRepository clientRepository, IPositionRepository positionRepository)
        {
            _clientRepository = clientRepository;
            _positionRepository = positionRepository;
        }
        public GetCountClient? CountClient()
        {
            var data = _clientRepository.GetAll();
            var clientdata = new GetCountClient();
            foreach (var client in data) 
            {
                if(client.IsAvailable == true)
                {
                    clientdata.CountAvailable++;
                }
                else
                {
                    clientdata.CountUnAvailable++;
                }
            }
            return clientdata;
        }
        public IEnumerable<GetAvailableClient> GetAllClient()
        {
            var data = from client in _clientRepository.GetAll()
                       join position in _positionRepository.GetAll() on client.Guid equals position.ClientGuid into positionGroup
                       from position in positionGroup.DefaultIfEmpty()
                       select new GetAvailableClient
                       {
                           Capacity = position!= null? position.Capacity:0,
                           PositionName = position != null ? position.Name:null,
                           Email = client.Email,
                           IsAvailable = client.IsAvailable,
                           Name = client.Name,
                       };
            /*var listclient = new List<GetAvailableClient>();
            foreach (var client in data) 
            {
                var countclient = new GetAvailableClient()
                {
                    PositionName = client.PositionName,
                    Capacity = client.Capacity,
                    Name = client.Name,
                    Email = client.Email,
                    IsAvailable = client.IsAvailable,
                };
                if(countclient.IsAvailable == true)
                {
                    listclient.Add(countclient);
                }
            }*/
            if(!data.Any())
            {
                return null;
            }
            return data;
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
            Client clienttoCreate = newClientDto;
            clienttoCreate.IsAvailable = false;
            var client = _clientRepository.Create(clienttoCreate);
            if (client is null)
            {
                return null; // Client is null or not found;
            }

            return (ClientDto)client; // Client is found;
        }

        public int Update(UpdateClientDto clientDto)
        {
            var client = _clientRepository.GetByGuid(clientDto.Guid);
            if (client is null)
            {
                return -1; // Client is null or not found;
            }
            var position = _positionRepository.GetByGuid(clientDto.Guid);
           
            Client toUpdate = clientDto;
            toUpdate.IsAvailable = position is null ? false : true;
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
