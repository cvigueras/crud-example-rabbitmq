using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Crud.Example.Domain.Events;

namespace Crud.Example.Domain.Entities
{
    [Table("Shop")]
    public class Shop
    {
        [Column("Id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }

        [Column("Name")]
        [Required]
        [StringLength(100)]
        public string? Name { get; set; }

        [Column("Address")]
        [Required]
        [StringLength(100)]
        public string? Address { get; set; }

        public DateTime ExpirationDateTime { get; set; }

        public bool Processed { get; set; }
        [JsonIgnore]
        public ICollection<Dealer>? Dealers { get; set; }

        public void ShopRemoved()
        {
            DomainEvent.Raise(new ShopRemovedEvent(DateTime.UtcNow, this));
        }

        public void ShopsExpired()
        {
            DomainEvent.Raise(new ShopExpiredEvent(this.ExpirationDateTime, this));
        }
    }
}