using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using TemporalBox.Models;
using System.Net.Http.Headers;
using Microsoft.IdentityModel.Tokens;


namespace TemporalBox.Controllers
{

  
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;
       
        // private readonly SignInManager<IdentityUser> _signInManager;

        
        public HomeController(ILogger<HomeController> logger , HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
           
            //_signInManager = signInManager;
        }

        public async Task<IActionResult> Index()
        {   
            
            return View();
        }

        public async Task<IActionResult> Login(LoginRequest logindetail)
        {
            if (logindetail != null)
              {
                var apiUrl = $"https://localhost:44373/api/JwtAuth/Login";
                var jsonvalue = JsonConvert.SerializeObject(logindetail);
                var content = new StringContent(jsonvalue, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    var responseToken = await response.Content.ReadAsStringAsync();

                    //storing token in cookie
                    HttpContext.Session.SetString("JWtToken", responseToken);
                    GlobalVariable.Token = HttpContext.Session.GetString("JWtToken");
                    HttpContext.Response.Cookies.Append("JwtToken", responseToken);


                    var handler = new JwtSecurityTokenHandler();
                    var token = handler.ReadJwtToken(responseToken);

                    //extracting user details from token
          

                    GlobalVariable.Role = token.Claims.FirstOrDefault(x => x.Type == "Role").Value;
                    GlobalVariable.Email = token.Claims.FirstOrDefault(x => x.Type == "Email").Value;
                    string userId = token.Claims.FirstOrDefault(x => x.Type == "Id").Value;


                    return RedirectToAction("Index");
                }
               
            }
           return View();
        }
      
        public async Task<IActionResult> SignUp(User user)
        {
            if (!user.Email.IsNullOrEmpty() && !user.Password.IsNullOrEmpty())
            {
                var Client = new HttpClient();
                var apiUrl = "https://localhost:44373/api/JwtAuth/AddUser";
                var jsonvalue = JsonConvert.SerializeObject(user);
                var content = new StringContent(jsonvalue, Encoding.UTF8, "application/json");
                var response =await  Client.PostAsync(apiUrl, content);

                return RedirectToAction("Login");

            }
            return View();
        }

        public IActionResult Logout()
        {
            
            HttpContext.Response.Cookies.Delete("JwtToken");
            GlobalVariable.Token = "";          
            return RedirectToAction("Login");
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
