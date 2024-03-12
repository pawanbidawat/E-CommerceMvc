using TemporalBox.Models;

namespace TemporalBox.Interfaces
{
    public interface IProducts
    {
       
        Task<Product> UploadImage(Product product);
        Task<Product> GetProductById(int id);
       
        Task<PagedProductData> GetProducts(Paging page);
        Task<Product> AddingProduct(Product product);
        Task<Product> UpdateProduct(Product product);
        Task DeleteProductImage(int id);
        Task<PagedProductData> FilterProducts(Paging paging);

    }
}
