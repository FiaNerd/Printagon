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
            .Include(o => o.OrderRolls)
            .ThenInclude(or => or.Roll)
            .FirstOrDefaultAsync(o => o.Id == orderId);
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders
                .Include(o => o.OrderRolls)
            .ToListAsync();
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            
            return order;
        }

        public async Task<Order?> UpdateOrderAsync(Guid orderId, Order order)
        {
            var existingOrder = await _context.Orders
                .Include(o => o.OrderRolls)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (existingOrder == null) 
            { 
                return null; 
            }

            existingOrder.OrderNumber = order.OrderNumber;
            existingOrder.JobName = order.JobName;
            existingOrder.YearlyNumber = order.YearlyNumber;
            existingOrder.PaperType = order.PaperType;
            existingOrder.PaperGramWeight = order.PaperGramWeight;
            existingOrder.PaperWidth = order.PaperWidth;
            existingOrder.OrderStatus = order.OrderStatus;
            existingOrder.Comment = order.Comment;

            await _context.SaveChangesAsync();

            return existingOrder;
        }

        public async Task<bool> DeleteOrderAsync(Guid orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);

            if (order == null)
            {
                return false; 
            }

             _context.Orders.Remove(order);

            await _context.SaveChangesAsync();

            return true; 
        }
    }
}
