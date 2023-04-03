using Crud.Example.Domain.Entities;
using Crud.Example.Domain.Services;
using Crud.Example.Main.Interfaces;

namespace Crud.Example.Main.Services
{
    public class CreateShopService : ICreateShopService
    {
        private readonly IShopService _shopService;

        public CreateShopService(IShopService shopService)
        {
            _shopService = shopService;
        }

        public int CreateShop(Shop shop)
        {
            return _shopService.CreateShop(shop);
        }
    }
}
