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
                .Include(r => r.Orders)
                .ToListAsync();
        }

        public Task<Roll?> GetRollByIdAsync(Guid rollId)
        {
            throw new NotImplementedException();
        }

        public Task<Roll> CreateRollAsync(Roll roll)
        {
            throw new NotImplementedException();
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
