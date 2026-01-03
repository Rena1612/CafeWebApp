using CafeWebApp.Models;
using System.Text.Json;

namespace CafeWebApp.Services
{
    /// <summary>
    /// Shopping cart service implementation using session storage
    /// </summary>
    public class CartService : ICartService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string CartSessionKey = "ShoppingCart";

        public CartService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private ISession? Session => _httpContextAccessor.HttpContext?.Session;

        public ShoppingCart GetCart()
        {
            var session = Session;
            if (session == null)
                return new ShoppingCart();

            var cartJson = session.GetString(CartSessionKey);
            if (string.IsNullOrEmpty(cartJson))
            {
                return new ShoppingCart();
            }

            return JsonSerializer.Deserialize<ShoppingCart>(cartJson) ?? new ShoppingCart();
        }

        private void SaveCart(ShoppingCart cart)
        {
            var session = Session;
            if (session != null)
            {
                var cartJson = JsonSerializer.Serialize(cart);
                session.SetString(CartSessionKey, cartJson);
            }
        }

        public void AddToCart(int productId, string productName, decimal price, string? imageUrl, int quantity = 1)
        {
            var cart = GetCart();
            var cartItem = new CartItem
            {
                ProductId = productId,
                ProductName = productName,
                Price = price,
                ImageUrl = imageUrl,
                Quantity = quantity
            };

            cart.AddItem(cartItem);
            SaveCart(cart);
        }

        public void UpdateCartItem(int productId, int quantity)
        {
            var cart = GetCart();
            cart.UpdateQuantity(productId, quantity);
            SaveCart(cart);
        }

        public void RemoveFromCart(int productId)
        {
            var cart = GetCart();
            cart.RemoveItem(productId);
            SaveCart(cart);
        }

        public void ClearCart()
        {
            var session = Session;
            session?.Remove(CartSessionKey);
        }

        public int GetCartItemCount()
        {
            var cart = GetCart();
            return cart.TotalItems;
        }

        public decimal GetCartTotal()
        {
            var cart = GetCart();
            return cart.Total;
        }
    }
}
