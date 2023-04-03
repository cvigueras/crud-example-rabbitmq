using Crud.Example.Domain.Entities;

namespace Crud.Example.Main.Interfaces
{
    public interface IGetDealerService
    {
        IEnumerable<Dealer> GetAllDealer();
    }
}
