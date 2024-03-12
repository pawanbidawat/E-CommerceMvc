using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using TemporalBox.Models;
using static System.Net.WebRequestMethods;

namespace TemporalBox.Controllers
{
    
    public class SharedController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public SharedController(HttpClient httpClient, IConfiguration configuration)
        {

            _httpClient = httpClient;
            _configuration = configuration;
        }


        public IActionResult Add()
        {
            return View();
        }

        public async Task<IActionResult> AllCategories()
        {

            var Client = new HttpClient();
            var apiUrl = $"{_configuration.GetSection("AccessUrl").Value}/Category/GetAllCategory";
            var token = GlobalVariable.Token;
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await Client.GetAsync(apiUrl);

            var data = JsonConvert.DeserializeObject<List<Categories>>(await response.Content.ReadAsStringAsync());

            return View(data);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var Clients = new HttpClient();
            var apiUrl = $"{_configuration.GetSection("AccessUrl").Value}/Category/GetCategory?id={id}";
            var response = await Clients.GetAsync(apiUrl);
            var data = JsonConvert.DeserializeObject<Categories>(await response.Content.ReadAsStringAsync());
            var dataList = new List<Categories> { data };


            return View(dataList);
        }

        public async Task<IActionResult> EditSubCategory(int id )
        {
            var Client = new HttpClient();
            var apiUrl = $"{_configuration.GetSection("AccessUrl").Value}/Category/GetSubCategoryWithId?id={id}";
            var response = await Client.GetAsync(apiUrl);
            var data=  JsonConvert.DeserializeObject<List<SubCategory>>(await response.Content.ReadAsStringAsync());
           
            ViewBag.subCategory = data;

            return View(data);
        }


        public async Task<IActionResult> GetSubCategory(int id)
        {
            var Client = new HttpClient();
            var apiUrl = $"{_configuration.GetSection("AccessUrl").Value}/Category/GetSubCategory?id={id}";
            var response = await Client.GetAsync(apiUrl);

            var data = JsonConvert.DeserializeObject<List<SubCategory>>(await response.Content.ReadAsStringAsync());
            ViewBag.SubcategoryList = data;
            return View(data);
        }

        public IActionResult AddCategory()
        {
            return View();
        }

      
    }
}
