using TemporalBox.Models;

namespace TemporalBox.Interfaces
{
    public interface ICart
    {
        Task<CartItem>addToCart (CartItem cartItem);
        Task<List<CartItem>> GetCartItem(string cartId);
    }
}
