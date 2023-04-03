using Crud.Example.Domain.Entities;

namespace Crud.Example.Domain.Events
{
    public class ShopRemovedEvent : IDomainEvent
    {
        public DateTime ShopCreateDate { get; private set; }

        public Shop? Shop;

        public ShopRemovedEvent(DateTime shopCreatedDate, Shop shop)
        {
            this.ShopCreateDate = shopCreatedDate;
            this.Shop = shop;
        }
    }
}
