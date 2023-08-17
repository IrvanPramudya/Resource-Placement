using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories
{
    public class GradeRepository : GeneralRepository<Grade>, IGradeRepository
    {
        public GradeRepository(PlacementDbContext context) : base(context) { }

    }
}
