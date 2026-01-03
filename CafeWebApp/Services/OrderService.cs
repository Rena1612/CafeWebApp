using CafeWebApp.Models;
using CafeWebApp.Repositories;
using CafeWebApp.ViewModels;

namespace CafeWebApp.Services
{
    /// <summary>
    /// Order service implementation
    /// </summary>
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Order> CreateOrderAsync(CheckoutViewModel model, string customerId, ShoppingCart cart)
        {
            // Create order
            var order = new Order
            {
                CustomerId = customerId,
                CustomerName = model.CustomerName,
                Phone = model.Phone,
                TableNumber = model.TableNumber,
                IsTakeaway = model.IsTakeaway,
                PaymentMethod = model.PaymentMethod,
                Notes = model.Notes,
                TotalAmount = cart.Total,
                Status = OrderStatus.Pending,
                CreatedAt = DateTime.Now
            };

            // Create order items
            foreach (var item in cart.Items)
            {
                var orderItem = new OrderItem
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    UnitPrice = item.Price,
                    Quantity = item.Quantity
                };
                order.OrderItems.Add(orderItem);
            }

            // Save order
            await _orderRepository.CreateAsync(order);
            return order;
        }

        public async Task<IEnumerable<Order>> GetCustomerOrdersAsync(string customerId)
        {
            return await _orderRepository.GetByCustomerAsync(customerId);
        }

        public async Task<Order?> GetOrderDetailsAsync(int orderId)
        {
            return await _orderRepository.GetByIdWithDetailsAsync(orderId);
        }

        public async Task UpdateOrderStatusAsync(int orderId, string status)
        {
            await _orderRepository.UpdateStatusAsync(orderId, status);
        }
    }
}
