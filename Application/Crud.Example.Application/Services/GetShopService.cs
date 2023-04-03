using Crud.Example.Domain.Entities;
using Crud.Example.Domain.Services;
using Crud.Example.Main.Interfaces;

namespace Crud.Example.Main.Services
{
    public class GetShopService : IGetShopService
    {
        private readonly IShopService _shopService;

        public GetShopService(IShopService shopService)
        {
            _shopService = shopService;
        }

        public IEnumerable<Shop> GetShops()
        {
            return _shopService.GetAllShops();
        }
    }
}
