using Microsoft.EntityFrameworkCore;
using Printagon.Api.Data;
using Printagon.Api.Models;
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

        public async Task<IEnumerable<OrderRoll>> GetRollsForOrderAsync(Guid orderId)
        {

            return await _context.OrderRolls
                .Where(or => or.OrderId == orderId)
                .Include(or => or.Order )
                .Include(or => or.Roll)
                .ToListAsync();
        }

        public async Task<OrderRoll?> GetByOrderAndRollAsync(Guid orderId, Guid rollId)
        {
            return await _context.OrderRolls
                .Where(or => or.OrderId == orderId && or.RollId == rollId)
                .Include(or => or.Order)
                .Include(or => or.Roll)
                .FirstOrDefaultAsync();
        }

        public async Task<OrderRoll> CreateOrderRollAsync(OrderRoll orderRoll)
        {
            await _context.OrderRolls.AddAsync(orderRoll);
            await _context.SaveChangesAsync();

            return orderRoll;
        }

        public Task<OrderRoll?> UpdateOrderRollAsync(OrderRoll orderRoll)
        {
            throw new NotImplementedException();
        }

        public Task DeleteOrderRollAsync(OrderRoll orderRoll)
        {
            throw new NotImplementedException();
        }
    }
}
