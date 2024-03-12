using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using TemporalBox.Interfaces;
using TemporalBox.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TemporalBox.Services
{
    public class Products : IProducts
    {
        private readonly IWebHostEnvironment _webHost;
        public Products(IWebHostEnvironment webHost)
        {

            _webHost = webHost;

        }

        public async Task<PagedProductData> GetProducts(Paging page)
        {
            var Client = new HttpClient();
            var apiUrl = $"https://localhost:44373/api/Product/GetAllProduct?page={page.currentPage}&min={page.min}&max={page.max}&ColumnName={page.ColumnName}";

            // Retrieve the token from the cookie
            var token = GlobalVariable.Token;
            // Add the token to the request headers
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await Client.GetAsync(apiUrl);

            var data = JsonConvert.DeserializeObject<PagedProductData>(await response.Content.ReadAsStringAsync());
            return data;
        }


        //product filter by range


        public async Task<PagedProductData> FilterProducts(Paging paging)
        {
            var Client = new HttpClient();
            var apiUrl = $"https://localhost:44373/api/Product/GetFilterProduct?page={paging.currentPage}&min={paging.min}&max={paging.max}";
            var response = await Client.GetAsync(apiUrl);
            var data = JsonConvert.DeserializeObject<PagedProductData>(await response.Content.ReadAsStringAsync());

            GlobalVariable.CurrentPage = data.Paging.currentPage;
            GlobalVariable.PageCount = data.Paging.pageCount;
            return data;

        }

        //get product by id
        public async Task<Product> GetProductById(int id)
        {
            var Client = new HttpClient();
            var apiUrl = $"https://localhost:44373/api/Product/GetProductById?id={id}";
            var response = await Client.GetAsync(apiUrl);
            var data = JsonConvert.DeserializeObject<Product>(await response.Content.ReadAsStringAsync());
            return data;
        }
        //adding product
        public async Task<Product> AddingProduct(Product product)
        {
            var data = await UploadImage(product);
            var Client = new HttpClient();
            var apiUrl = "https://localhost:44373/api/Product/AddProduct";
            var jsonvalue = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonvalue, Encoding.UTF8, "application/json");
            var response = await Client.PostAsync(apiUrl, content);
            return product;
        }

        //updaing product
        public async Task<Product> UpdateProduct(Product product)
        {
            if (product.ProductImageFile != null)
            {
                var imageData = await UploadImage(product);
                var client = new HttpClient();
                var apiUrl = "https://localhost:44373/api/Product/UpdateProduct";
                var jsonValue = JsonConvert.SerializeObject(imageData);
                var content = new StringContent(jsonValue, Encoding.UTF8, "application/json");
                var response = await client.PutAsync(apiUrl, content);

            }
            else
            {
                var client = new HttpClient();
                var apiUrl = "https://localhost:44373/api/Product/UpdateProduct";
                var jsonValue = JsonConvert.SerializeObject(product);
                var content = new StringContent(jsonValue, Encoding.UTF8, "application/json");
                var response = await client.PutAsync(apiUrl, content);

            }

            return product;
        }

        //deleting product image
        public async Task DeleteProductImage(int id)
        {
            var data = await GetProductById(id);

            var Client = new HttpClient();
            var apiUrl = $"https://localhost:44373/api/Product/DeleteProduct?id={id}";
            var response = await Client.DeleteAsync(apiUrl);

            if (data.ProductImage != null)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", data.ProductImage);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

            }
            return;
        }
        public async Task<Product> UploadImage(Product product)
        {
            if (product.ProductImage != null)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", product.ProductImage);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

            }
            if (product.ProductImageFile != null && product.ProductImageFile.Length > 0)
            {

                var uploadFolder = Path.Combine(_webHost.WebRootPath, "images");
                var fileName = product.ProductName.ToString() + "." + product.ProductImageFile.FileName;
                var filePath = Path.Combine(uploadFolder, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await product.ProductImageFile.CopyToAsync(fileStream);
                }
                product.ProductImage = fileName;
            }
            return product;
        }
    }
}
