using Printagon.Api.DTOs.Roll;
using Printagon.Api.Models;
using Printagon.Api.Repositories.Interfaces;
using Printagon.Api.Services.Interfaces;

namespace Printagon.Api.Services
{
    public class RollService : IRollService
    {
        private readonly IRollRepository _rollRepo;
        private readonly IOrderRepository _orderRepo;

        public RollService(IRollRepository rollRepo, IOrderRepository orderRepo)
        {
            _rollRepo = rollRepo;
            _orderRepo = orderRepo;
        }

            public async Task<IEnumerable<RollResponseDto>> GetAllRollsAsync()
            {
                var rolls = await _rollRepo.GetAllRollsAsync();

                    return rolls.Select(r => new RollResponseDto
                    {
                        Id = r.Id,
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

        public async Task<RollResponseDto?> GetRollByIdAsync(Guid rollId)
        {
           var roll = await _rollRepo.GetRollByIdAsync(rollId);

            if (roll == null)
            { 
                throw new KeyNotFoundException($"Roll with ID {rollId} not found.");
            }

            var rollResponse = new RollResponseDto
            {
                Id = roll.Id,
                RollNumber = roll.RollNumber,
                PaperType = roll.PaperType,
                GramWeight = roll.GramWeight,
                RollWidth = roll.RollWidth,
                RollWeight = roll.RollWeight,
                RollWeightLeftOver = roll.RollWeightLeftOver,
                Comment = roll.Comment,
                CreatedBy = null,
                CreatedAt = roll.CreatedAt
            };

            return rollResponse;
        }

        public async Task<RollResponseDto> CreateRollAsync(Guid orderId, RollCreateDto roll)
        {
            var order = await _orderRepo.GetOrderByIdAsync(orderId);
            
            if (order == null)
            {
                throw new KeyNotFoundException($"Order with ID {orderId} not found.");
            }

            var newRoll = new Roll
            {
                Id = Guid.NewGuid(),
                RollNumber = roll.RollNumber,
                RollWeight = roll.RollWeight,
                RollWeightLeftOver = roll.RollWeightLeftOver,
                Comment = roll.Comment,

                PaperType = roll.PaperTypeOverride ?? order.PaperType,
                GramWeight = roll.GramWeightOverride ?? order.GramWeight,
                RollWidth = roll.RollWidthOverride ?? order.RollWidth,

                CreatedAt = DateTime.UtcNow
            };

            var createdRoll = await _rollRepo.CreateRollAsync(newRoll);

            var rollResponse = new RollResponseDto
            {
                Id = newRoll.Id,
                RollNumber = newRoll.RollNumber,
                PaperType = newRoll.PaperType,
                GramWeight = newRoll.GramWeight,
                RollWidth = newRoll.RollWidth,
                RollWeight = newRoll.RollWeight,
                RollWeightLeftOver = newRoll.RollWeightLeftOver,
                Comment = newRoll.Comment,
                CreatedBy = null,
                CreatedAt = newRoll.CreatedAt
            };

            return rollResponse;
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
