using Crud.Example.Domain.Entities;
using Crud.Example.Domain.Repositories;
using Crud.Example.Domain.Services;

namespace Crud.Example.Domain.Core.Services
{
    public class ShopService : BaseService<Shop>, IShopService
    {
        #region Private Members
        private readonly IShopRepository _shopRepository;
        #endregion
        public ShopService(IShopRepository shopRepository) : base(shopRepository)
        {
            _shopRepository = shopRepository;
        }

        public int CreateShop(Shop shop)
        {
            return _shopRepository.Add(shop);
        }
        
        public IEnumerable<Shop> GetAllShops()
        {
            return _shopRepository.GetAll();
        }

        public bool RemoveShopByName(string name)
        {
            if(name == null)
            {
                return false;
            }
            var shop = _shopRepository.RemoveShopByName(name);
            if (shop != null)
            {
                shop.ShopRemoved();
            }
            return shop != null;
        }

        public void GetShopsExpirated()
        {
            var shops = _shopRepository.GetAllShopsExpirated();
            if(shops!= null && shops.Count > 0)
            {
                foreach(var shop in shops)
                {
                    shop.Processed = true;
                    _shopRepository.Modify(shop);
                    shop.ShopsExpired();
                }
            }
        }
    }
}

