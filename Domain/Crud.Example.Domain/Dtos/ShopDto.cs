namespace Crud.Example.Domain.Dtos
{
    public class ShopDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public string? Address { get; set; }
        
        public bool Processed { get; set; }
        public ICollection<DealerDto>? Dealers { get; set; }
        
        public ICollection<OrderDto>? Order { get; set; }
    }
}
