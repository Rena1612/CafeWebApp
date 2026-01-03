using CafeWebApp.Services;
using CafeWebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CafeWebApp.Controllers
{
    /// <summary>
    /// Checkout controller - handles order checkout process
    /// </summary>
    [Authorize(Roles = "Customer,Admin")]
    public class CheckoutController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;

        public CheckoutController(ICartService cartService, IOrderService orderService)
        {
            _cartService = cartService;
            _orderService = orderService;
        }

        /// <summary>
        /// Display checkout form
        /// </summary>
        public IActionResult Index()
        {
            var cart = _cartService.GetCart();
            
            if (cart.Items.Count == 0)
            {
                TempData["Error"] = "Your cart is empty";
                return RedirectToAction("Index", "Menu");
            }

            // Pre-fill customer info if authenticated
            var model = new CheckoutViewModel
            {
                CustomerName = User.Identity?.Name ?? string.Empty,
                Phone = User.FindFirst(ClaimTypes.MobilePhone)?.Value ?? string.Empty,
                TotalAmount = cart.Total,
                TotalItems = cart.TotalItems
            };

            return View(model);
        }

        /// <summary>
        /// Process checkout and create order
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PlaceOrder(CheckoutViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var cart = _cartService.GetCart();
                model.TotalAmount = cart.Total;
                model.TotalItems = cart.TotalItems;
                return View("Index", model);
            }

            var shoppingCart = _cartService.GetCart();
            if (shoppingCart.Items.Count == 0)
            {
                TempData["Error"] = "Your cart is empty";
                return RedirectToAction("Index", "Menu");
            }

            // Get current user ID
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            // Create order
            var order = await _orderService.CreateOrderAsync(model, userId, shoppingCart);

            // Clear cart
            _cartService.ClearCart();

            // Redirect to confirmation
            return RedirectToAction("Confirmation", new { id = order.Id });
        }

        /// <summary>
        /// Display order confirmation
        /// </summary>
        public async Task<IActionResult> Confirmation(int id)
        {
            var order = await _orderService.GetOrderDetailsAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            // Verify the order belongs to current user (or is admin)
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (order.CustomerId != userId && !User.IsInRole("Admin"))
            {
                return Forbid();
            }

            return View(order);
        }

        /// <summary>
        /// Display user's order history
        /// </summary>
        public async Task<IActionResult> MyOrders()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            var orders = await _orderService.GetCustomerOrdersAsync(userId);
            return View(orders);
        }

        /// <summary>
        /// View order details
        /// </summary>
        public async Task<IActionResult> OrderDetails(int id)
        {
            var order = await _orderService.GetOrderDetailsAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            // Verify the order belongs to current user (or is admin)
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (order.CustomerId != userId && !User.IsInRole("Admin"))
            {
                return Forbid();
            }

            return View(order);
        }
    }
}
