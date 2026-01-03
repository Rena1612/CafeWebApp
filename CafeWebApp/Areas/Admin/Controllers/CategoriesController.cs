using CafeWebApp.Models;
using CafeWebApp.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CafeWebApp.Areas.Admin.Controllers
{
    /// <summary>
    /// Admin categories controller - CRUD for categories
    /// </summary>
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoriesController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        /// <summary>
        /// List all categories
        /// </summary>
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return View(categories);
        }

        /// <summary>
        /// Display create category form
        /// </summary>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Create new category
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                await _categoryRepository.CreateAsync(category);
                TempData["Success"] = "Category created successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        /// <summary>
        /// Display edit category form
        /// </summary>
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        /// <summary>
        /// Update category
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _categoryRepository.UpdateAsync(category);
                TempData["Success"] = "Category updated successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        /// <summary>
        /// Display delete confirmation
        /// </summary>
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        /// <summary>
        /// Delete category
        /// </summary>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category != null)
            {
                if (category.Products.Any())
                {
                    TempData["Error"] = "Cannot delete category with existing products";
                    return RedirectToAction(nameof(Index));
                }

                await _categoryRepository.DeleteAsync(id);
                TempData["Success"] = "Category deleted successfully";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
