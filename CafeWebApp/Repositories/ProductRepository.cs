using CafeWebApp.Data;
using CafeWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CafeWebApp.Repositories
{
    /// <summary>
    /// Product repository implementation
    /// </summary>
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products
                .Include(p => p.Category)
                .OrderBy(p => p.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetInStockAsync()
        {
            return await _context.Products
                .Include(p => p.Category)
                .Where(p => p.InStock)
                .OrderBy(p => p.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetFeaturedAsync()
        {
            return await _context.Products
                .Include(p => p.Category)
                .Where(p => p.IsFeatured && p.InStock)
                .OrderBy(p => p.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetByCategoryAsync(int categoryId)
        {
            return await _context.Products
                .Include(p => p.Category)
                .Where(p => p.CategoryId == categoryId && p.InStock)
                .OrderBy(p => p.Name)
                .ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product> CreateAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Products.AnyAsync(p => p.Id == id);
        }
    }
}
