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
                .ToListAsync();
        }

        public Task<OrderRoll?> GetByOrderAndRollAsync(Guid orderId, Guid rollId)
        {
            throw new NotImplementedException();
        }
        public Task AddOrderRollAsync(OrderRoll orderRoll)
        {
            throw new NotImplementedException();
        }

        public Task UpdateOrderRollAsync(OrderRoll orderRoll)
        {
            throw new NotImplementedException();
        }

        public Task DeleteOrderRollAsync(OrderRoll orderRoll)
        {
            throw new NotImplementedException();
        }
    }
}
