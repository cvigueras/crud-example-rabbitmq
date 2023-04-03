using Crud.Example.Domain.Entities;

namespace Crud.Example.Domain.Events
{
    public class ShopExpiredEvent : IDomainEvent
    {
        public DateTime ShopExpirationDate { get; private set; }

        public Shop? Shop;

        public ShopExpiredEvent(DateTime shopExpirationDate, Shop shop)
        {
            this.ShopExpirationDate = ShopExpirationDate;
            this.Shop = shop;
        }
    }
}
