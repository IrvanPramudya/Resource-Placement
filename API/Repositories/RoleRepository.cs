using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories
{
    public class RoleRepository : GeneralRepository<Role>, IRoleRepository
    {
        public RoleRepository(PlacementDbContext context) : base(context) { }
        public bool IsNotExist(string value)
        {
            return _context.Set<Role>().SingleOrDefault(role => role.Name.Contains(value)) is null;
        }

    }
}
