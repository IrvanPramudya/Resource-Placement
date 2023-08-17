using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories
{
    public class ClientRepository : GeneralRepository<Client>, IClientRepository
    {
        public ClientRepository(PlacementDbContext context) : base(context) { }

    }
}
