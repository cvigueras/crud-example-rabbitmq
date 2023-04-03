using Crud.Example.Domain.Entities;

namespace Crud.Example.Main.Interfaces
{
    public interface IGetShopService
    {
        IEnumerable<Shop> GetShops();
    }
}
