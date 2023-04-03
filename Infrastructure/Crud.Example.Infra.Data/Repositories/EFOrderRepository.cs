using Crud.Example.Domain.Entities;
using Crud.Example.Domain.Repositories;
using Crud.Example.Infrastructure.Data.Context;

namespace Crud.Example.Infrastructure.Data.Repositories
{
    public class EFOrderRepository : EFBaseRepository<Order>, IOrderRepository
    {
        public EFOrderRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
