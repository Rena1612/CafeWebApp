using CafeWebApp.Data;
using CafeWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CafeWebApp.Repositories
{
    /// <summary>
    /// Order repository implementation
    /// </summary>
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .Include(o => o.Customer)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetByCustomerAsync(string customerId)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .Where(o => o.CustomerId == customerId)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();
        }

        public async Task<Order?> GetByIdAsync(int id)
        {
            return await _context.Orders
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<Order?> GetByIdWithDetailsAsync(int id)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .Include(o => o.Customer)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<Order> CreateAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<Order> UpdateAsync(Order order)
        {
            order.UpdatedAt = DateTime.Now;
            _context.Entry(order).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task UpdateStatusAsync(int id, string status)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                order.Status = status;
                order.UpdatedAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Orders.AnyAsync(o => o.Id == id);
        }
    }
}
