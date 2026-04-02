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
                        PaperGramWeight = r.PaperGramWeight,
                        PaperWidth = r.PaperWidth,
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
                PaperGramWeight = roll.PaperGramWeight,
                PaperWidth = roll.PaperWidth,
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
                PaperGramWeight = roll.PaperGramWeightOverride ?? order.PaperGramWeight,
                PaperWidth = roll.RollWidthOverride ?? order.PaperWidth,

                CreatedAt = DateTime.UtcNow
            };

            var createdRoll = await _rollRepo.CreateRollAsync(newRoll);

            var rollResponse = new RollResponseDto
            {
                Id = newRoll.Id,
                RollNumber = newRoll.RollNumber,
                PaperType = newRoll.PaperType,
                PaperGramWeight = newRoll.PaperGramWeight,
                PaperWidth = newRoll.PaperWidth,
                RollWeight = newRoll.RollWeight,
                RollWeightLeftOver = newRoll.RollWeightLeftOver,
                Comment = newRoll.Comment,
                CreatedBy = null,
                CreatedAt = newRoll.CreatedAt
            };

            return rollResponse;
        }

      
        public async Task<RollResponseDto?> UpdateRollAsync(Guid orderId, Guid rollId, RollUpdateDto updatedRoll)
        {
            var order = await _orderRepo.GetOrderByIdAsync(orderId);

            if (order == null)
            {
                throw new KeyNotFoundException($"Order with ID {orderId} not found.");
            }

            var existingRoll = await _rollRepo.GetRollByIdAsync(rollId);

            if(existingRoll == null) 
            { 
                throw new KeyNotFoundException($"Roll with ID {rollId} not found.");
            }

            if (existingRoll.Id != orderId)
            { 
                throw new InvalidOperationException($"Roll with ID {rollId} does not belong to Order with ID {orderId}.");
            }

            existingRoll.RollNumber = updatedRoll.RollNumber;
            existingRoll.RollWeight = updatedRoll.RollWeight;
            existingRoll.RollWeightLeftOver = updatedRoll.RollWeightLeftOver;
            existingRoll.Comment = updatedRoll.Comment;

            existingRoll.PaperType = updatedRoll.PaperType ?? existingRoll.PaperType;
            existingRoll.PaperGramWeight = updatedRoll.PaperGramWeight ?? existingRoll.PaperGramWeight;
            existingRoll.PaperWidth = updatedRoll.PaperWidth ?? existingRoll.PaperWidth;


            var updated = await _rollRepo.UpdateRollAsync(existingRoll);

           var newResponse = new RollResponseDto
            {
                Id = updated.Id,
                RollNumber = updated.RollNumber,
                PaperType = updated.PaperType,
                PaperGramWeight = updated.PaperGramWeight,
                PaperWidth = updated.PaperWidth,
                RollWeight = updated.RollWeight,
                RollWeightLeftOver = updated.RollWeightLeftOver,
                Comment = updated.Comment,
                CreatedBy = null,
                CreatedAt = updated.CreatedAt
            };

            return newResponse;

        }

        public async Task DeleteRollAsync(Guid rollId)
        {
            var roll = await _rollRepo.GetRollByIdAsync(rollId);

            if (roll == null)
            {
                throw new KeyNotFoundException($"Roll with ID {rollId} not found.");
            }
             
            await _rollRepo.DeleteRollAsync(rollId);
        }

    }
}
