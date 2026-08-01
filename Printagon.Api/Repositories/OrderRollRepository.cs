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

        public async Task<IEnumerable<OrderRoll>> GetAllByOrderNumberAsync(int orderNumber)
        {
            return await _context.OrderRolls
                .Where(or => or.OrderNumber == orderNumber)
                .ToListAsync();
        }

        public async Task<OrderRoll?> GetByIdAsync(Guid id)
        {
            return await _context.OrderRolls
                .FirstOrDefaultAsync(or => or.Id == id);
        }

        public async Task<OrderRoll?> GetByOrderAndRollAsync(int orderNumber, int rollNumber)
        {
            return await _context.OrderRolls
                .FirstOrDefaultAsync(or =>
                    or.OrderNumber == orderNumber &&
                    or.RollNumber == rollNumber
                );
        }

        public async Task<OrderRoll> AddAsync(OrderRoll orderRoll)
        {
            await _context.OrderRolls.AddAsync(orderRoll);
            await _context.SaveChangesAsync();
            return orderRoll;
        }

        public async Task<bool> UpdateAsync(OrderRoll orderRoll)
        {
            _context.OrderRolls.Update(orderRoll);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(OrderRoll orderRoll)
        {
            _context.OrderRolls.Remove(orderRoll);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
