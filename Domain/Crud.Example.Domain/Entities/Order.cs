using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Crud.Example.Domain.Entities
{
    [Table("Orders")]
    public class Order
    {
        //public Order()
        //{
        //    FoodOrders = new HashSet<FoodOrder>();
        //}
        public int Id { get; set; }

        public DateTime DateOrder { get; set; }

        public string? Address { get; set; }

        public DateTime ExpirationDateTime { get; set; }

        [JsonIgnore]
        public List<Food>? Foods { get; set; }

        [JsonIgnore]
        public Dealer? Dealer { get; set; }
    }
}