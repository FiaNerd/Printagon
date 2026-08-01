
using Printagon.Api.Models;
using Printagon.Api.Repositories.Interfaces;
using Printagon.Api.Services.Interfaces;

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

    public async Task<RollResponseDto?> GetRollByNumberAsync(int rollNumber)
    {
        var roll = await _rollRepo.GetRollByNumberAsync(rollNumber);

        if (roll == null)
            throw new KeyNotFoundException($"Roll with ID {rollNumber} not found.");

        return new RollResponseDto
        {
            RollNumber = roll.RollNumber,
            PaperType = roll.PaperType,
            PaperGramWeight = roll.PaperGramWeight,
            PaperWidth = roll.PaperWidth,
            RollWeight = roll.RollWeight,
            RollWeightLeftOver = roll.RollWeightLeftOver,
            Comment = roll.Comment,
            CreatedBy = roll.CreatedBy,
            CreatedAt = roll.CreatedAt
        };
    }

    public async Task<RollResponseDto> CreateRollAsync(int orderNumber, RollCreateDto roll)
    {
        var order = await _orderRepo.GetOrderByNumberAsync(orderNumber);

        if (order == null)
            throw new KeyNotFoundException($"Order with ID {orderNumber} not found.");

        var newRoll = new Roll
        {
            RollNumber = roll.RollNumber,
            RollWeight = roll.RollWeight,
            RollWeightLeftOver = roll.RollWeightLeftOver,
            Comment = roll.Comment,

            PaperType = roll.PaperTypeOverride ?? order.PaperType,
            PaperGramWeight = roll.PaperGramWeightOverride ?? order.PaperGramWeight,
            PaperWidth = roll.RollWidthOverride ?? order.PaperWidth,

            CreatedBy = roll.CreatedBy,
            CreatedAt = DateTime.UtcNow
        };

        var createdRoll = await _rollRepo.CreateRollAsync(newRoll);

        return new RollResponseDto
        {
            RollNumber = createdRoll.RollNumber,
            PaperType = createdRoll.PaperType,
            PaperGramWeight = createdRoll.PaperGramWeight,
            PaperWidth = createdRoll.PaperWidth,
            RollWeight = createdRoll.RollWeight,
            RollWeightLeftOver = createdRoll.RollWeightLeftOver,
            Comment = createdRoll.Comment,
            CreatedBy = createdRoll.CreatedBy,
            CreatedAt = createdRoll.CreatedAt
        };
    }

    public async Task<RollResponseDto?> UpdateRollAsync(int orderNumber, int rollNumber, RollUpdateDto updatedRoll)
    {
        var order = await _orderRepo.GetOrderByNumberAsync(orderNumber);

        if (order == null)
            throw new KeyNotFoundException($"Order with ID {orderNumber} not found.");

        var existingRoll = await _rollRepo.GetRollByNumberAsync(rollNumber);

        if (existingRoll == null)
            throw new KeyNotFoundException($"Roll with ID {rollNumber} not found.");

        existingRoll.RollWeight = updatedRoll.RollWeight;
        existingRoll.RollWeightLeftOver = updatedRoll.RollWeightLeftOver;
        existingRoll.Comment = updatedRoll.Comment;

        existingRoll.PaperType = updatedRoll.PaperType ?? existingRoll.PaperType;
        existingRoll.PaperGramWeight = updatedRoll.PaperGramWeight ?? existingRoll.PaperGramWeight;
        existingRoll.PaperWidth = updatedRoll.PaperWidth ?? existingRoll.PaperWidth;

        var updated = await _rollRepo.UpdateRollAsync(existingRoll);

        return new RollResponseDto
        {
            RollNumber = updated.RollNumber,
            PaperType = updated.PaperType,
            PaperGramWeight = updated.PaperGramWeight,
            PaperWidth = updated.PaperWidth,
            RollWeight = updated.RollWeight,
            RollWeightLeftOver = updated.RollWeightLeftOver,
            Comment = updated.Comment,
            CreatedBy = updated.CreatedBy,
            CreatedAt = updated.CreatedAt
        };
    }

    public async Task DeleteRollAsync(int rollNumber)
    {
        var roll = await _rollRepo.GetRollByNumberAsync(rollNumber);

        if (roll == null)
            throw new KeyNotFoundException($"Roll with ID {rollNumber} not found.");

        await _rollRepo.DeleteRollAsync(rollNumber);
    }
}
