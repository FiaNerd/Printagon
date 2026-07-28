using Microsoft.EntityFrameworkCore;
using Printagon.Api.DTOs.Order;
using Printagon.Api.DTOs.OrderRoll;
using Printagon.Api.Repositories;
using Printagon.Api.Repositories.Interfaces;

namespace Printagon.Api.Services
{
        public class OrderRollService : IOrderRollService
        {
            private readonly IOrderRollRepository _orderRollRepository;

            public OrderRollService(IOrderRollRepository orderRollRepository)
            {
                _orderRollRepository = orderRollRepository;
            }

            public async Task<IEnumerable<OrderRollResponseDto>> GetAllByOrderIdAsync(Guid orderId)
            {
                var orderRolls = await _orderRollRepository.GetAllByOrderIdAsync(orderId);
                return orderRolls.Select(MapToDto).ToList();
            }

            public async Task<OrderRollResponseDto?> GetByIdAsync(Guid orderRollId)
            {
                var orderRoll = await _orderRollRepository.GetByIdAsync(orderRollId);
                return orderRoll == null ? null : MapToDto(orderRoll);
            }

            public async Task<OrderRollResponseDto?> GetByOrderAndRollAsync(Guid orderId, Guid rollId)
            {
                var orderRoll = await _orderRollRepository.GetByOrderAndRollAsync(orderId, rollId);
                return orderRoll == null ? null : MapToDto(orderRoll);
            }

            public async Task<OrderRollResponseDto> AddAsync(OrderRollCreateDto dto)
            {
                var newOrderRoll = new OrderRoll
                {
                    Id = Guid.NewGuid(),
                    OrderId = dto.OrderId,
                    RollId = dto.RollId,
                    IntakeWeight = dto.IntakeWeight,
                    PaperType = dto.PaperType,
                    PaperGramWeight = dto.PaperGramWeight,
                    PaperWidth = dto.PaperWidth,
                    IsRestRoll = dto.IsRestRoll,
                    DeviationReason = dto.DeviationReason,
                    WebBreakCount = dto.WebBreakCount,
                    CreatedAt = DateTime.UtcNow
                };

                await _orderRollRepository.AddAsync(newOrderRoll);

                return MapToDto(newOrderRoll);
            }

            public async Task<OrderRollResponseDto?> UpdateAsync(Guid id, OrderRollUpdateDto dto)
            {
                var existing = await _orderRollRepository.GetByIdAsync(id);

                if (existing == null)
                    return null;

                existing.OutputWeight = dto.OutputWeight ?? existing.OutputWeight;
                existing.MatchesOrderPaper = dto.MatchesOrderPaper ?? existing.MatchesOrderPaper;
                existing.DeviationReason = dto.DeviationReason ?? existing.DeviationReason;
                existing.IsRestRoll = dto.IsRestRoll ?? existing.IsRestRoll;
                existing.WebBreakCount = dto.WebBreakCount ?? existing.WebBreakCount;

                await _orderRollRepository.UpdateAsync(existing);

                return MapToDto(existing);
            }

            public async Task<bool> DeleteAsync(Guid id)
            {
                var orderRoll = await _orderRollRepository.GetByIdAsync(id);
                if (orderRoll == null)
                    return false;

                return await _orderRollRepository.DeleteAsync(orderRoll);
            }

            private static OrderRollResponseDto MapToDto(OrderRoll or)
            {
                return new OrderRollResponseDto
                {
                    Id = or.Id,
                    OrderId = or.OrderId,
                    RollId = or.RollId,
                    IntakeWeight = or.IntakeWeight,
                    OutputWeight = or.OutputWeight,
                    ConsumedWeight = or.ConsumedWeight,
                    PaperType = or.PaperType,
                    PaperGramWeight = or.PaperGramWeight,
                    PaperWidth = or.PaperWidth,
                    MatchesOrderPaper = or.MatchesOrderPaper,
                    DeviationReason = or.DeviationReason,
                    WebBreakCount = or.WebBreakCount,
                    IsRestRoll = or.IsRestRoll,
                    CreatedAt = or.CreatedAt
                };
            }
        }

    }

