namespace TemporalBox.Models
{
    public class Paging
    {
        //public List<Product> Products { get; set; }
        public int pageCount { get; set; }
        public int currentPage { get; set; }
        public int page { get; set; }
        public int totalPages { get; set; }
        public string? search { get; set; }
        public int min { get; set; }
        public int max { get; set; }
        public string? ColumnName { get; set; } = "default";
    }
}
