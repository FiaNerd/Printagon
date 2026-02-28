using Microsoft.EntityFrameworkCore;
using Printagon.Api.Data;
using Printagon.Api.Models;
using Printagon.Api.Repositories.Interfaces;

namespace Printagon.Api.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Order?> GetOrderByIdAsync(Guid orderId)
        {
           return await _context.Orders
            .Include(o => o.Rolls)
            .FirstOrDefaultAsync(o => o.Id == orderId);
        }

        public Task<IEnumerable<Order>> GetOrdersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Order> CreateOrderAsync(Order order)
        {
            throw new NotImplementedException();
        }
        public Task<Order?> UpdateOrderAsync(Order order)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteOrderAsync(Guid orderId)
        {
            throw new NotImplementedException();
        }

    }
}
