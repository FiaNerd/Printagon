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

            public async Task<IEnumerable<OrderRollResponseDto>> GetAllByOrderNumberAsync(int orderNumber)
            {
                var orderRolls = await _orderRollRepository.GetAllByOrderNumberAsync(orderNumber);
                return orderRolls.Select(MapToDto).ToList();
            }

            public async Task<OrderRollResponseDto?> GetByIdAsync(Guid orderRollNumber)
            {
                var orderRoll = await _orderRollRepository.GetByIdAsync(orderRollNumber);
                return orderRoll == null ? null : MapToDto(orderRoll);
            }

            public async Task<OrderRollResponseDto?> GetByOrderAndRollAsync(int orderNumber, int rollNumber)
            {
                var orderRoll = await _orderRollRepository.GetByOrderAndRollAsync(orderNumber, rollNumber);
                return orderRoll == null ? null : MapToDto(orderRoll);
            }

            public async Task<OrderRollResponseDto> AddAsync(int orderNumber, OrderRollCreateDto dto)
            {
                var newOrderRoll = new OrderRoll
                {
                    OrderNumber = orderNumber,
                    RollNumber = dto.RollNumber,
                    IntakeWeight = dto.IntakeWeight,
                    PaperType = dto.PaperType,
                    PaperGramWeight = dto.PaperGramWeight,
                    PaperWidth = dto.PaperWidth,
                    IsRestRoll = dto.IsRestRoll,
                    DeviationReason = dto.DeviationReason,
                    WebBreakCount = dto.WebBreakCount
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
                    OrderNumber = or.OrderNumber,
                    RollNumber = or.RollNumber,
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

