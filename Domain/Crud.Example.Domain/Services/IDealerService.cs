using Crud.Example.Domain.Entities;

namespace Crud.Example.Domain.Services
{
    public interface IDealerService : IBaseService<Dealer>
    {
        void CreateDealer(Dealer dealer);
    }
}