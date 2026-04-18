using Microsoft.EntityFrameworkCore;
using Printagon.Api.Data;
using Printagon.Api.Models;
using Printagon.Api.Repositories.Interfaces;

namespace Printagon.Api.Repositories
{
    public class RollRepository : IRollRepository
    {
        private readonly ApplicationDbContext _context;

        public RollRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Roll>> GetAllRollsAsync()
        {
            return await _context.Rolls
                .Include(r => r.OrderRolls)
                .ToListAsync();
        }

        public async Task<Roll?> GetRollByIdAsync(Guid rollId)
        {
            return await _context.Rolls
                .Include(r => r.OrderRolls)
                .FirstOrDefaultAsync(r => r.Id == rollId);
        }

        public async Task<Roll> CreateRollAsync(Roll roll)
        {
            await _context.Rolls.AddAsync(roll);
            await _context.SaveChangesAsync();

            return roll;
        }

        public async Task<Roll?> UpdateRollAsync(Roll updatedRoll)
        {
            var existingRoll = await _context.Rolls.FindAsync(updatedRoll.Id);

            if (existingRoll == null)
            {
                return null;
            }

            existingRoll.PaperType = updatedRoll.PaperType;
            existingRoll.PaperGramWeight = updatedRoll.PaperGramWeight;
            existingRoll.PaperWidth = updatedRoll.PaperWidth;
            existingRoll.RollNumber = updatedRoll.RollNumber;
            existingRoll.RollWeight = updatedRoll.RollWeight;
            existingRoll.Comment = updatedRoll.Comment;

            await _context.SaveChangesAsync();

            return existingRoll;
        }

        public async Task<bool> DeleteRollAsync(Guid rollId)
        {
            var roll = await _context.Rolls.FindAsync(rollId);

            if (roll == null)
            {
                return false;
            }

            _context.Rolls.Remove(roll);

            await _context.SaveChangesAsync();

            return true;

        }


    }
}
