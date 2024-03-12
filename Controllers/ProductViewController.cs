using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;
using TemporalBox.Interfaces;
using TemporalBox.Models;
using TemporalBox.Services;


namespace TemporalBox.Controllers
{
    public class ProductViewController : Controller
     {
        private readonly IWebHostEnvironment _webHost;
        private readonly IProducts _products;
  

        public ProductViewController(IWebHostEnvironment webHost, IProducts products)
        {
            _webHost = webHost;
            _products = products;
         
        }

        public IActionResult AddProduct()
        {
            return View();
        }

        
        public async Task<IActionResult> ShowProducts(Paging page )
        {
            var data = await  _products.GetProducts(page);
            GlobalVariable.CurrentPage = data.Paging.currentPage;
            GlobalVariable.PageCount = data.Paging.pageCount;

            var viewData = new PagedProductData
            {
                PagedData = data.PagedData,
                Paging = data.Paging
            };
           

            return View(viewData);

        }

        //partial View Method for product list
        public async Task <PartialViewResult> _ProductList(Paging paging)
        {
            var data = await _products.GetProducts(paging);

            var viewData = new PagedProductData
            {
                PagedData = data.PagedData,
                Paging = data.Paging
            };

            return PartialView(viewData);
        }

        //adding method for product detail

        public async Task<IActionResult> ProductDetail(int id)
        {
            var data = await _products.GetProductById(id);
            return View(data);
        }

        //adding filter to product list
        public async Task<PartialViewResult> FilterProducts(Paging paging)
        {
            if (paging.min != 0 || paging.max != 0 ) { 
              var data = await _products.FilterProducts(paging);
            
                var viewData = new PagedProductData
                {
                    PagedData = data.PagedData,
                    Paging = data.Paging
                };

                return PartialView("_ProductList", viewData);

            }
            else
            {
                return PartialView("_ProductList");
            }
        }

        [HttpPost]
        [ActionName("AddingProduct")]
        public async Task<IActionResult> AddingProduct(Product product)
        {
            var data = await _products.AddingProduct(product);
            return RedirectToAction("AddProduct");      
        }


        public async Task<IActionResult> EditProduct(int id)
        {
            var data = await _products.GetProductById(id);
            return View(data);
        }

        public async Task<IActionResult> UpdateProduct(Product product)
        {
            var data = await _products.UpdateProduct(product);
            return RedirectToAction("ShowProducts");


        }

        public async Task<IActionResult> DeleteProduct(int id )
        {             
            await _products.DeleteProductImage(id);
            return RedirectToAction("ShowProducts");

        }
    }
}
