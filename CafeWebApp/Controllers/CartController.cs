using CafeWebApp.Repositories;
using CafeWebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace CafeWebApp.Controllers
{
    /// <summary>
    /// Shopping cart controller
    /// </summary>
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IProductRepository _productRepository;

        public CartController(ICartService cartService, IProductRepository productRepository)
        {
            _cartService = cartService;
            _productRepository = productRepository;
        }

        /// <summary>
        /// Display shopping cart
        /// </summary>
        public IActionResult Index()
        {
            var cart = _cartService.GetCart();
            return View(cart);
        }

        /// <summary>
        /// Add item to cart
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId, int quantity = 1)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null || !product.InStock)
            {
                TempData["Error"] = "Product not available";
                return RedirectToAction("Index", "Menu");
            }

            _cartService.AddToCart(product.Id, product.Name, product.Price, product.ImageUrl, quantity);
            TempData["Success"] = $"{product.Name} added to cart";

            return RedirectToAction("Index", "Menu");
        }

        /// <summary>
        /// Update cart item quantity
        /// </summary>
        [HttpPost]
        public IActionResult UpdateQuantity(int productId, int quantity)
        {
            _cartService.UpdateCartItem(productId, quantity);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Remove item from cart
        /// </summary>
        [HttpPost]
        public IActionResult RemoveItem(int productId)
        {
            _cartService.RemoveFromCart(productId);
            TempData["Success"] = "Item removed from cart";
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Clear entire cart
        /// </summary>
        [HttpPost]
        public IActionResult ClearCart()
        {
            _cartService.ClearCart();
            TempData["Success"] = "Cart cleared";
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Get cart item count (for header badge)
        /// </summary>
        public IActionResult GetCartCount()
        {
            var count = _cartService.GetCartItemCount();
            return Json(new { count });
        }
    }
}
