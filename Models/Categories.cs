using System.ComponentModel.DataAnnotations;
using TemporalBox.Models;

namespace TemporalBox.Models
{
    public class Categories
    {
        [Key]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<SubCategory> SubCategoryList { get; set; }
    }
}
