using CafeWebApp.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CafeWebApp.Controllers
{
    /// <summary>
    /// Menu controller - displays products and menu
    /// </summary>
    public class MenuController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public MenuController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        /// <summary>
        /// Display all menu items
        /// </summary>
        public async Task<IActionResult> Index(int? categoryId)
        {
            // Get active categories for filter
            ViewBag.Categories = await _categoryRepository.GetActiveAsync();
            ViewBag.SelectedCategoryId = categoryId;

            IEnumerable<CafeWebApp.Models.Product> products;

            if (categoryId.HasValue && categoryId.Value > 0)
            {
                // Get products by category
                products = await _productRepository.GetByCategoryAsync(categoryId.Value);
                var category = await _categoryRepository.GetByIdAsync(categoryId.Value);
                ViewBag.CategoryName = category?.Name;
            }
            else
            {
                // Get all in-stock products
                products = await _productRepository.GetInStockAsync();
            }

            return View(products);
        }

        /// <summary>
        /// Display product details
        /// </summary>
        public async Task<IActionResult> Details(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
    }
}
