using CafeWebApp.Models;
using CafeWebApp.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CafeWebApp.Areas.Admin.Controllers
{
    /// <summary>
    /// Admin orders controller - view and manage orders
    /// </summary>
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class OrdersController : Controller
    {
        private readonly IOrderRepository _orderRepository;

        public OrdersController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        /// <summary>
        /// List all orders
        /// </summary>
        public async Task<IActionResult> Index(string? status)
        {
            var orders = await _orderRepository.GetAllAsync();

            if (!string.IsNullOrEmpty(status))
            {
                orders = orders.Where(o => o.Status == status);
            }

            ViewBag.SelectedStatus = status;
            return View(orders);
        }

        /// <summary>
        /// View order details
        /// </summary>
        public async Task<IActionResult> Details(int id)
        {
            var order = await _orderRepository.GetByIdWithDetailsAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        /// <summary>
        /// Update order status
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStatus(int id, string status)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            await _orderRepository.UpdateStatusAsync(id, status);
            TempData["Success"] = $"Order status updated to {status}";

            return RedirectToAction(nameof(Details), new { id });
        }
    }
}
