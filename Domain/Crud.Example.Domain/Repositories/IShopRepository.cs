using Crud.Example.Domain.Entities;

namespace Crud.Example.Domain.Repositories
{
    public interface IShopRepository : IBaseRepository<Shop>
    {
        Shop? RemoveShopByName(string name);
        List<Shop>? GetAllShopsExpirated();
    }
}
