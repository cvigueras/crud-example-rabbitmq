using Crud.Example.Domain.Entities;
using Crud.Example.Domain.Repositories;
using Crud.Example.Infrastructure.Data.Context;

namespace Crud.Example.Infrastructure.Data.Repositories
{
    public class EFFoodRepository : EFBaseRepository<Food>, IFoodRepository
    {
        public EFFoodRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
