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
                .Include(or => or.Roll)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<OrderRoll?> GetByIdAsync(Guid orderRollId)
        {
            return await _context.OrderRolls
                .Include(or => or.Order)
                .Include(or => or.Roll)
                .FirstOrDefaultAsync(or => or.Id == orderRollId);
        }

        public async Task<OrderRoll?> GetByOrderAndRollAsync(Guid orderId, Guid rollId)
        {
            return await _context.OrderRolls
                .Include(or => or.Order)
                .Include(or => or.Roll)
                .FirstOrDefaultAsync(or => or.OrderId == orderId && or.RollId == rollId);
        }
  
  
        public async Task AddAsync(OrderRoll orderRoll)
        {
             await _context.OrderRolls
                .AddAsync(orderRoll);
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


        //public async Task<IEnumerable<OrderRoll>> GetOrderRollsByOrderIdAsync(Guid orderId)
        //{
        //    return await _context.OrderRolls
        //        .Where(or => or.OrderId == orderId)
        //        .Include(or => or.Roll)
        //        .AsNoTracking()
        //        .ToListAsync();
        //}

        //public async Task<OrderRoll?> GetOrderRollByIdAsync(Guid orderRollId)
        //{
        //    return await _context.OrderRolls
        //        .FirstOrDefaultAsync(or => or.Id == orderRollId);
        //}

        //public async Task<OrderRoll?> GetOrderRollByOrderIdAndRollIdAsync(Guid orderId, Guid rollId)
        //{
        //    return await _context.OrderRolls
        //        .Where(or => or.OrderId == orderId && or.RollId == rollId)
        //        .Include(or => or.Order)
        //        .Include(or => or.Roll)
        //        .FirstOrDefaultAsync();
        //}

        //public async Task<OrderRoll> AddAsync(OrderRoll newOrderRoll)
        //{
        //     _context.OrderRolls.AddAsync(newOrderRoll);
        //    _context.SaveChangesAsync();

        //    return newOrderRoll;
        //}

        //public  Task<OrderRoll?> Update(OrderRoll updatedOrderRoll)
        //{
        //    var existingOrderRoll =  _context.OrderRolls.FindAsync(updatedOrderRoll.Id);

        //    if (existingOrderRoll == null)
        //    {
        //        return null;
        //    }

        //    existingOrderRoll.OrderId = updatedOrderRoll.OrderId;
        //    existingOrderRoll.RollId = updatedOrderRoll.RollId;
        //    existingOrderRoll.IntakeWeight = updatedOrderRoll.IntakeWeight;
        //    existingOrderRoll.OutputWeight = updatedOrderRoll.OutputWeight;
        //    existingOrderRoll.MatchesOrderPaper = updatedOrderRoll.MatchesOrderPaper;
        //    existingOrderRoll.DeviationReason = updatedOrderRoll.DeviationReason;
        //    existingOrderRoll.IsRestRoll = updatedOrderRoll.IsRestRoll;
        //    existingOrderRoll.WebBreakCount = updatedOrderRoll.WebBreakCount;
        //     _context.SaveChangesAsync();

        //    return existingOrderRoll;
        //}

        //public async Task (Guid orderRollId)
        //{
        //    var orderRoll = await _context.OrderRolls.FindAsync(orderRollId);

        //    if (orderRoll == null)
        //    {
        //        return false;
        //    }

        //    _context.OrderRolls.Remove(orderRoll);
        //    await _context.SaveChangesAsync();

        //    return true;
        //}
    }
}
