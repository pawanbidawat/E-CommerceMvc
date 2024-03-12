namespace TemporalBox.Models
{
    public class SubCategory
    {
        public int SubCategoryId { get; set; }
        public int CategoryId { get; set; }  // defining the foreign key
        public string? SubCategoryName { get; set; }
        public Categories Category { get; set; }
    }
}
