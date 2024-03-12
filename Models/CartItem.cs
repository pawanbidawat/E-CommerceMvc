using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TemporalBox.Models
{
    public class CartItem
    {
        [Key]
        public int ItemId { get; set; }

        public string? CartId { get; set; }

        public int Quantity { get; set; }

        public DateTime DateCreated { get; set; }

        public int ProductId { get; set; }
        public decimal Price { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product? Product { get; set; }
    }
}
