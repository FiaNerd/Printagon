using Printagon.Api.DTOs.Roll;
using Printagon.Api.Models;
using Printagon.Api.Repositories.Interfaces;
using Printagon.Api.Services.Interfaces;

namespace Printagon.Api.Services
{
    public class RollService : IRollService
    {
        private readonly IRollRepository _rollRepo;

        public RollService(IRollRepository rollRepo)
        {
            _rollRepo = rollRepo;
        }

            public async Task<IEnumerable<RollResponseDto>> GetAllRollsAsync()
            {
                var rolls = await _rollRepo.GetAllRollsAsync();

                return rolls.Select(r => new RollResponseDto
                {
                    RollNumber = r.RollNumber,
                    PaperType = r.PaperType,
                    GramWeight = r.GramWeight,
                    RollWidth = r.RollWidth,
                    RollWeight = r.RollWeight,
                    RollWeightLeftOver = r.RollWeightLeftOver,
                    Comment = r.Comment,
                    CreatedBy = r.CreatedBy,
                    CreatedAt = r.CreatedAt
                });
            }

        public async Task<Roll?> GetRollByIdAsync(Guid rollId)
        {
           var roll = await _rollRepo.GetRollByIdAsync(rollId);

            if (roll == null)
            { 
                throw new KeyNotFoundException($"Roll with ID {rollId} not found.");
            }

            return roll;
        }

        public Task<Roll> CreateRollAsync(Roll roll)
        {
            throw new NotImplementedException();
        }

      
        public Task<Roll?> UpdateRollAsync(Guid rollId, Roll updatedRoll)
        {
            throw new NotImplementedException();
        }

        public Task<bool?> DeleteRollAsync(Guid rollId)
        {
            throw new NotImplementedException();
        }

    }
}
