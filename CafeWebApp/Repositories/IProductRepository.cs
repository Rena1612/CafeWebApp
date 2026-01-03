using CafeWebApp.Models;

namespace CafeWebApp.Repositories
{
    /// <summary>
    /// Product repository interface
    /// </summary>
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<IEnumerable<Product>> GetInStockAsync();
        Task<IEnumerable<Product>> GetFeaturedAsync();
        Task<IEnumerable<Product>> GetByCategoryAsync(int categoryId);
        Task<Product?> GetByIdAsync(int id);
        Task<Product> CreateAsync(Product product);
        Task<Product> UpdateAsync(Product product);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
