using CafeWebApp.Models;

namespace CafeWebApp.Repositories
{
    /// <summary>
    /// Category repository interface
    /// </summary>
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<IEnumerable<Category>> GetActiveAsync();
        Task<Category?> GetByIdAsync(int id);
        Task<Category> CreateAsync(Category category);
        Task<Category> UpdateAsync(Category category);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
