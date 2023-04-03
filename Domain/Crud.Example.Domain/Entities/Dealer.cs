using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Crud.Example.Domain.Entities
{
    [Table("Dealers")]
    public class Dealer
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

        [Column("Surname")]
        [Required]
        [StringLength(100)]
        public string? Surname { get; set; }

        [JsonIgnore]
        public virtual Shop? Shop { get; set; }

        [JsonIgnore]
        public ICollection<Order>? Orders { get; set; }
    }
}