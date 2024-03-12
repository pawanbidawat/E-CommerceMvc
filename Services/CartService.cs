using Newtonsoft.Json;
using System.Text;
using TemporalBox.Interfaces;
using TemporalBox.Models;

namespace TemporalBox.Services
{
    public class CartService : ICart
    {
        public async Task<CartItem> addToCart(CartItem cartItem)
        {
            var Client = new HttpClient();
            var apiUrl = "https://localhost:44373/api/Cart/AddToCart";
            var jsonCartItem = JsonConvert.SerializeObject(cartItem);
            var content = new StringContent(jsonCartItem, Encoding.UTF8, "application/json");

            var response = await Client.PostAsync(apiUrl, content);

            return cartItem;
        }



        public async Task<List<CartItem>> GetCartItem(string cartId)
        {

            var Client = new HttpClient();
            var apiUrl = $"https://localhost:44373/api/Cart/GetCart?cartId={cartId}";
            var response = await Client.GetAsync(apiUrl);
            var responseContent = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<List<CartItem>>(responseContent);
            return data;

        }

    }
}
