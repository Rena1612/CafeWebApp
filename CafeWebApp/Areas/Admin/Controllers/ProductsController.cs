using CafeWebApp.Models;
using CafeWebApp.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CafeWebApp.Areas.Admin.Controllers
{
    /// <summary>
    /// Admin products controller - CRUD for products
    /// </summary>
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductsController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductsController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        /// <summary>
        /// List all products
        /// </summary>
        public async Task<IActionResult> Index()
        {
            var products = await _productRepository.GetAllAsync();
            return View(products);
        }

        /// <summary>
        /// Display create product form
        /// </summary>
        public async Task<IActionResult> Create()
        {
            await LoadCategoriesAsync();
            return View();
        }

        /// <summary>
        /// Create new product
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                product.CreatedDate = DateTime.Now;
                await _productRepository.CreateAsync(product);
                TempData["Success"] = "Product created successfully";
                return RedirectToAction(nameof(Index));
            }
            await LoadCategoriesAsync();
            return View(product);
        }

        /// <summary>
        /// Display edit product form
        /// </summary>
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            await LoadCategoriesAsync();
            return View(product);
        }

        /// <summary>
        /// Update product
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _productRepository.UpdateAsync(product);
                TempData["Success"] = "Product updated successfully";
                return RedirectToAction(nameof(Index));
            }
            await LoadCategoriesAsync();
            return View(product);
        }

        /// <summary>
        /// Display delete confirmation
        /// </summary>
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        /// <summary>
        /// Delete product
        /// </summary>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productRepository.DeleteAsync(id);
            TempData["Success"] = "Product deleted successfully";
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Helper method to load categories for dropdown
        /// </summary>
        private async Task LoadCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
        }
    }
}
