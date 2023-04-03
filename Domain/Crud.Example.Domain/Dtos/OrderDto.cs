namespace Crud.Example.Domain.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }

        public DateTime DateOrder { get; set; }

        public string? Address { get; set; }

        public virtual List<FoodDto>? Foods { get; set; }

        public  DealerDto? Dealer { get; set; }

        public ShopDto? Shop { get; set; }
    }
}
