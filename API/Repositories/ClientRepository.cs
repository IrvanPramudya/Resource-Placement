using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories
{
    public class ClientRepository : GeneralRepository<Client>, IClientRepository
    {
        public ClientRepository(PlacementDbContext context) : base(context) { }
        public bool IsNotExist(string value)
        {
            return _context.Set<Client>()
                            .SingleOrDefault(e => e.Email.Contains(value)) is null;
        }

    }
}
