using CafeWebApp.Models;

namespace CafeWebApp.Services
{
    /// <summary>
    /// Shopping cart service interface
    /// </summary>
    public interface ICartService
    {
        ShoppingCart GetCart();
        void AddToCart(int productId, string productName, decimal price, string? imageUrl, int quantity = 1);
        void UpdateCartItem(int productId, int quantity);
        void RemoveFromCart(int productId);
        void ClearCart();
        int GetCartItemCount();
        decimal GetCartTotal();
    }
}
