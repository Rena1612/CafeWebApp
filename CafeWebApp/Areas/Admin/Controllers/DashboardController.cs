using CafeWebApp.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CafeWebApp.Areas.Admin.Controllers
{
    /// <summary>
    /// Admin dashboard controller
    /// </summary>
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public DashboardController(
            IOrderRepository orderRepository,
            IProductRepository productRepository,
            ICategoryRepository categoryRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        /// <summary>
        /// Admin dashboard home
        /// </summary>
        public async Task<IActionResult> Index()
        {
            // Get statistics
            var allOrders = await _orderRepository.GetAllAsync();
            var allProducts = await _productRepository.GetAllAsync();
            var allCategories = await _categoryRepository.GetAllAsync();

            ViewBag.TotalOrders = allOrders.Count();
            ViewBag.TotalProducts = allProducts.Count();
            ViewBag.TotalCategories = allCategories.Count();
            ViewBag.PendingOrders = allOrders.Count(o => o.Status == "Pending");
            ViewBag.TotalRevenue = allOrders.Sum(o => o.TotalAmount);

            // Recent orders
            var recentOrders = allOrders.Take(10);

            return View(recentOrders);
        }
    }
}
