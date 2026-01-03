using CafeWebApp.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CafeWebApp.Controllers
{
    /// <summary>
    /// Home controller - handles home page and general pages
    /// </summary>
    public class HomeController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IProductRepository productRepository, ILogger<HomeController> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        /// <summary>
        /// Home page with featured products
        /// </summary>
        public async Task<IActionResult> Index()
        {
            var featuredProducts = await _productRepository.GetFeaturedAsync();
            return View(featuredProducts);
        }

        /// <summary>
        /// About page
        /// </summary>
        public IActionResult About()
        {
            return View();
        }

        /// <summary>
        /// Contact page
        /// </summary>
        public IActionResult Contact()
        {
            return View();
        }

        /// <summary>
        /// Privacy policy page
        /// </summary>
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Error page
        /// </summary>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}
