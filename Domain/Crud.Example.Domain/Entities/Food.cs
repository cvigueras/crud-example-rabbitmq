using System.Text.Json.Serialization;

namespace Crud.Example.Domain.Entities
{
    public class Food/* : Entity*/
    {
        //public Food()
        //{
        //    FoodOrders = new HashSet<FoodOrder>();
        //}

        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Size { get; set; }
        public double? Price { get; set; }

        [JsonIgnore]
        public List<Order>? Orders { get; set; }
    }
}