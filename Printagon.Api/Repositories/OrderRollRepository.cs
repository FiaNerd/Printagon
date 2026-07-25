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


        public async Task<IEnumerable<OrderRoll>> GetAllByOrderIdAsync(Guid orderId)
        {
            return await _context.OrderRolls
                .Where(or => or.OrderId == orderId)
                .ToListAsync();
        }


        public async Task<OrderRoll?> GetByIdAsync(Guid orderRollId)
        {
            return await _context.OrderRolls
                .FirstOrDefaultAsync(or => or.Id == orderRollId);
        }


        public async Task<OrderRoll?> GetByOrderAndRollAsync(Guid orderId, Guid rollId)
        {
            return await _context.OrderRolls
                .Include(or => or.Order)
                .Include(or => or.Roll)
                .FirstOrDefaultAsync(or => or.OrderId == orderId && or.RollId == rollId);
        }

        public async Task<OrderRoll> AddAsync(OrderRoll orderRoll)
        {
            await _context.OrderRolls.AddAsync(orderRoll);

            await _context.SaveChangesAsync();

            return orderRoll;
        }

        public void Update(OrderRoll orderRoll)
        {

            _context.OrderRolls.Update(orderRoll);
        }

        public void Remove(OrderRoll orderRoll)
        {
            _context.OrderRolls.Remove(orderRoll);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
