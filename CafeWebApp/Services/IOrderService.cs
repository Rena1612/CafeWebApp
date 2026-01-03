using CafeWebApp.Models;
using CafeWebApp.ViewModels;

namespace CafeWebApp.Services
{
    /// <summary>
    /// Order service interface
    /// </summary>
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(CheckoutViewModel model, string customerId, ShoppingCart cart);
        Task<IEnumerable<Order>> GetCustomerOrdersAsync(string customerId);
        Task<Order?> GetOrderDetailsAsync(int orderId);
        Task UpdateOrderStatusAsync(int orderId, string status);
    }
}
