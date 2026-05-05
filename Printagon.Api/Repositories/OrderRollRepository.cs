using Microsoft.EntityFrameworkCore;
using Printagon.Api.Data;
using Printagon.Api.Repositories.Interfaces;

namespace Printagon.Api.Repositories
{
    public class OrderRollRepository : IOrderRollRepository
    {
        private readonly ApplicationDbContext _context;
        public OrderRollRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderRoll>> GetOrderRollsByOrderIdAsync(Guid orderId)
        {
            return await _context.OrderRolls
                .Where(or => or.OrderId == orderId)
                .Include(or => or.Order)
                .Include(or => or.Roll)
                .ToListAsync();
        }

        public async Task<OrderRoll?> GetOrderRollByIdAsync(Guid orderRollId)
        {
            return await _context.OrderRolls
                .Include(or => or.Order)
                .Include(or => or.Roll)
                .FirstOrDefaultAsync(or => or.Id == orderRollId);
        }

        public async Task<OrderRoll?> GetOrderRollByOrderIdAndRollIdAsync(Guid orderId, Guid rollId)
        {
            return await _context.OrderRolls
                .Where(or => or.OrderId == orderId && or.RollId == rollId)
                .Include(or => or.Order)
                .Include(or => or.Roll)
                .FirstOrDefaultAsync();
        }

        public async Task<OrderRoll> CreateOrderRollAsync(OrderRoll newOrderRoll)
        {
            await _context.OrderRolls.AddAsync(newOrderRoll);
            await _context.SaveChangesAsync();

            return newOrderRoll;
        }

        public async Task<OrderRoll?> UpdateOrderRollAsync(OrderRoll updatedOrderRoll)
        {
            var existingOrderRoll = await _context.OrderRolls.FindAsync(updatedOrderRoll.Id);
         
            if (existingOrderRoll == null)
            {
                return null;
            }

            existingOrderRoll.OrderId = updatedOrderRoll.OrderId;
            existingOrderRoll.RollId = updatedOrderRoll.RollId;
            existingOrderRoll.IntakeWeight = updatedOrderRoll.IntakeWeight;
            existingOrderRoll.OutputWeight = updatedOrderRoll.OutputWeight;
            existingOrderRoll.MatchesOrderPaper = updatedOrderRoll.MatchesOrderPaper;
            existingOrderRoll.DeviationReason = updatedOrderRoll.DeviationReason;
            existingOrderRoll.IsRestRoll = updatedOrderRoll.IsRestRoll;
            existingOrderRoll.WebBreak = updatedOrderRoll.WebBreak;

            await _context.SaveChangesAsync();

            return existingOrderRoll;
        }

        public async Task<bool> DeleteOrderRollByIdAsync(Guid orderRollId)
        {
            var orderRoll = await _context.OrderRolls.FindAsync(orderRollId);

            if (orderRoll == null)
            {
                return false;
            }

            _context.OrderRolls.Remove(orderRoll);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
