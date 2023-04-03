using Crud.Example.Domain.Entities;
using Crud.Example.Domain.Repositories;
using Crud.Example.Infrastructure.Data.Context;

namespace Crud.Example.Infrastructure.Data.Repositories
{
    public class EFDealerRepository : EFBaseRepository<Dealer>, IDealerRepository
    {
        public EFDealerRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
