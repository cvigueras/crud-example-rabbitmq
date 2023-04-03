using Crud.Example.Domain.Entities;

namespace Crud.Example.Domain.Services
{
    public interface IShopService : IBaseService<Shop>
    {
        int CreateShop(Shop shop);
        IEnumerable<Shop> GetAllShops();
        bool RemoveShopByName(string name);
        void GetShopsExpirated();
    }
}
