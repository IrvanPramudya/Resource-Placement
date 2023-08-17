using API.Models;

namespace API.Contracts
{
    public interface IClientRepository : IGeneralRepository<Client>
    {
        public bool IsNotExist(string value);
    }
}
