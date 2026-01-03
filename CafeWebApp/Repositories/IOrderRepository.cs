using CafeWebApp.Models;

namespace CafeWebApp.Repositories
{
    /// <summary>
    /// Order repository interface
    /// </summary>
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllAsync();
        Task<IEnumerable<Order>> GetByCustomerAsync(string customerId);
        Task<Order?> GetByIdAsync(int id);
        Task<Order?> GetByIdWithDetailsAsync(int id);
        Task<Order> CreateAsync(Order order);
        Task<Order> UpdateAsync(Order order);
        Task UpdateStatusAsync(int id, string status);
        Task<bool> ExistsAsync(int id);
    }
}
