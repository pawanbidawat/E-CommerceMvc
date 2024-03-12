using System.ComponentModel.DataAnnotations;

namespace TemporalBox.Models
{
    public class Product
    {
      
            [Key]
            public int ProductId { get; set; }
            public string? ProductName { get; set; }
            public string? ProductImage { get; set; }
            public IFormFile? ProductImageFile { get; set; }
            public decimal ProductPrice { get; set; }
            public string? ProductDescription { get; set; }
            public int CategoryId { get; set; }
            public int SubCategoryId { get; set; }
        
    }
}
