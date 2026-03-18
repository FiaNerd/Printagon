using Microsoft.EntityFrameworkCore;
using Printagon.Api.Data;
using Printagon.Api.Models;
using Printagon.Api.Repositories.Interfaces;
using System;

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
                .Include(r => r.Orders)
                .ToListAsync();
        }

        public async Task<Roll?> GetRollByIdAsync(Guid rollId)
        {
            Console.WriteLine($"Fetching roll with ID: {rollId}");

            return await _context.Rolls
                .Include(r => r.Orders)
                .FirstOrDefaultAsync(r => r.Id == rollId);
        }

        public async Task<Roll> CreateRollAsync(Roll roll)
        {
            await _context.Rolls.AddAsync(roll);
            await _context.SaveChangesAsync();

            return roll;
        }

        public Task<Roll?> UpdateRollAsync(Guid rollId, Roll updatedRoll)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteRollAsync(Guid rollId)
        {
            throw new NotImplementedException();
        }


    }
}
