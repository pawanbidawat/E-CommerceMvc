using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Nodes;
using System.Text;
using TemporalBox.Interfaces;
using TemporalBox.Models;

namespace TemporalBox.Controllers
{
    public class CartController : Controller
    {
        private readonly ICart _cart;
        public CartController(ICart cart)
        {
            _cart = cart;
        }
        public async Task<IActionResult> CartIndex(CartItem cartItem)
        {
            if (cartItem.ProductId != 0 && !string.IsNullOrEmpty(cartItem.CartId))
            {
                cartItem.Quantity = 1;
                //adding cartItem
                var data = await _cart.addToCart(cartItem);
            }
            else if (!string.IsNullOrEmpty(cartItem.CartId))
            {
                //getting cart list
                var CartList = await _cart.GetCartItem(cartItem.CartId);
                return View(CartList);
            }

            cartItem.CartId = GlobalVariable.Email;
            var cartList = await _cart.GetCartItem(cartItem.CartId);

            if(cartList.Count!=0)
            {

                return View(cartList);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public async void UpdateCart(int itemId , int quantity ,decimal totalPrice)
        {
            var Client = new HttpClient();
            if (itemId != 0 && quantity != 0)
            {
                var apiUrl = "https://localhost:44373/api/Cart/UpdateCart";
                var parameters = new CartItem { ItemId = itemId, Quantity = quantity , Price=totalPrice };
                var jsonPayload = JsonConvert.SerializeObject(parameters);
                var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
                var response = await Client.PutAsync(apiUrl, content);
            }
        }




    }

      
    }

