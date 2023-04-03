using Crud.Example.Domain.Services;
using Crud.Example.Main.Interfaces;

namespace Crud.Example.Main.Services
{
    public class RemoveShopService : IRemoveShopService
    {
        private readonly IShopService _shopService;

        public RemoveShopService(IShopService shopService)
        {
            _shopService = shopService;
        }
        public bool RemoveShopByName(string name)
        {
            return _shopService.RemoveShopByName(name);
        }
    }
}
