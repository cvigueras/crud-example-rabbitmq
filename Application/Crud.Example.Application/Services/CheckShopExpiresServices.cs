using Crud.Example.Domain.Services;
using Crud.Example.Main.Interfaces;

namespace Crud.Example.Main.Services
{
    public class CheckShopExpiresServices : ICheckShopExpiresServices
    {
        private readonly IShopService _shopService;

        public CheckShopExpiresServices(IShopService shopService)
        {
            _shopService = shopService;
        }

        public void CheckExpirationShops()
        {
            _shopService.GetShopsExpirated();
        }
    }
}
