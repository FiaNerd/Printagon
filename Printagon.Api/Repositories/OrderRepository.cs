using Microsoft.AspNetCore.Mvc;
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

        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            return await _context.Orders
                .Include(o => o.Rolls)
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
                .Include(o => o.Rolls)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (existingOrder == null) 
            { 
                return null; 
            }

            existingOrder.OrderNumber = order.OrderNumber;
            existingOrder.JobName = order.JobName;
            existingOrder.YearlyNumber = order.YearlyNumber;
            existingOrder.PaperType = order.PaperType;
            existingOrder.GramWeight = order.GramWeight;
            existingOrder.RollWidth = order.RollWidth;
            existingOrder.OrderStatus = order.OrderStatus;
            existingOrder.Comment = order.Comment;

            await _context.SaveChangesAsync();

            return existingOrder;
        }

        public async Task<bool> DeleteOrderAsync(Guid orderId)
        {
            _context.Orders.Remove(new Order { Id = orderId });

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
