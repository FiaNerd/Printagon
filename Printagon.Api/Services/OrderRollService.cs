using Microsoft.EntityFrameworkCore;
using Printagon.Api.DTOs.OrderRoll;
using Printagon.Api.Repositories;
using Printagon.Api.Repositories.Interfaces;

namespace Printagon.Api.Services
{
    public class OrderRollService : IOrderRollService
    {
        private readonly IOrderRollRepository _orderRollRepository;

        public OrderRollService(IOrderRollRepository orderRepository)
        {
            _orderRollRepository = orderRepository;
        }


        public async Task<IEnumerable<OrderRollResponseDto>> GetAllByOrderIdAsync(Guid orderId)
        {
            var orderRolls = await _orderRollRepository.GetAllByOrderIdAsync(orderId);

            return orderRolls.Select(or => new OrderRollResponseDto
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
            });
        }


        public Task<OrderRollResponseDto?> GetByIdAsync(Guid orderRollId)
        {
            var orderRoll = _orderRollRepository.GetByIdAsync(orderRollId);

           var orderRollDto = orderRoll.Result;
            if (orderRollDto == null)
            {
                return Task.FromResult<OrderRollResponseDto?>(null);
            }
            var responseDto = new OrderRollResponseDto
            {
                Id = orderRollDto.Id,
                OrderId = orderRollDto.OrderId,
                RollId = orderRollDto.RollId,
                IntakeWeight = orderRollDto.IntakeWeight,
                OutputWeight = orderRollDto.OutputWeight,
                ConsumedWeight = orderRollDto.ConsumedWeight,
                PaperType = orderRollDto.PaperType,
                PaperGramWeight = orderRollDto.PaperGramWeight,
                PaperWidth = orderRollDto.PaperWidth,
                MatchesOrderPaper = orderRollDto.MatchesOrderPaper,
                DeviationReason = orderRollDto.DeviationReason,
                WebBreakCount = orderRollDto.WebBreakCount,
                IsRestRoll = orderRollDto.IsRestRoll,
                CreatedAt = orderRollDto.CreatedAt
            };

            return Task.FromResult<OrderRollResponseDto?>(responseDto);
        }

        public Task<OrderRollResponseDto?> GetByOrderAndRollAsync(Guid orderId, Guid rollId)
        {
            var orderRoll = _orderRollRepository.GetByOrderAndRollAsync(orderId, rollId);

            var orderRollDto = orderRoll.Result;

            if (orderRollDto == null)
            {
                return Task.FromResult<OrderRollResponseDto?>(null);
            }

            var responseDto = new OrderRollResponseDto
            {
                Id = orderRollDto.Id,
                OrderId = orderRollDto.OrderId,
                RollId = orderRollDto.RollId,
                IntakeWeight = orderRollDto.IntakeWeight,
                OutputWeight = orderRollDto.OutputWeight,
                ConsumedWeight = orderRollDto.ConsumedWeight,
                PaperType = orderRollDto.PaperType,
                PaperGramWeight = orderRollDto.PaperGramWeight,
                PaperWidth = orderRollDto.PaperWidth,
                MatchesOrderPaper = orderRollDto.MatchesOrderPaper,
                DeviationReason = orderRollDto.DeviationReason,
                WebBreakCount = orderRollDto.WebBreakCount,
                IsRestRoll = orderRollDto.IsRestRoll,
                CreatedAt = orderRollDto.CreatedAt
            };

            return Task.FromResult<OrderRollResponseDto?>(responseDto);
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
                WebBreakCount = dto.WebBreakCount
            };

            await _orderRollRepository.AddAsync(newOrderRoll);

            return new OrderRollResponseDto
            {
                Id = newOrderRoll.Id,
                OrderId = newOrderRoll.OrderId,
                RollId = newOrderRoll.RollId,
                IntakeWeight = newOrderRoll.IntakeWeight,
                OutputWeight = newOrderRoll.OutputWeight,
                ConsumedWeight = newOrderRoll.ConsumedWeight,
                PaperType = newOrderRoll.PaperType,
                PaperGramWeight = newOrderRoll.PaperGramWeight,
                PaperWidth = newOrderRoll.PaperWidth,
                MatchesOrderPaper = newOrderRoll.MatchesOrderPaper,
                DeviationReason = newOrderRoll.DeviationReason,
                WebBreakCount = newOrderRoll.WebBreakCount,
                IsRestRoll = newOrderRoll.IsRestRoll,
                CreatedAt = newOrderRoll.CreatedAt
            };
        }


        public Task<OrderRollResponseDto?> UpdateAsync(Guid id, OrderRollUpdateDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveAsync(Guid id)
        {
            throw new NotImplementedException();
        }

   


        //public async Task<OrderRollResponseDto?> GetOrderAndRollByIdAsync(Guid orderId, Guid rollId)
        //{
        //    var orderRoll = await _orderRollRepository.GetOrderRollByOrderIdAndRollIdAsync(orderId, rollId);

        //    if (orderRoll == null)
        //    {
        //        return null;
        //    }

        //    return new OrderRollResponseDto
        //    {
        //        Id = orderRoll.Id,
        //        OrderId = orderRoll.OrderId,
        //        RollId = orderRoll.RollId,
        //        IntakeWeight = orderRoll.IntakeWeight,
        //        OutputWeight = orderRoll.OutputWeight,
        //        ConsumedWeight = orderRoll.ConsumedWeight,

        //        RollNumber = orderRoll.Roll?.RollNumber,
        //        PaperType = orderRoll.Roll?.PaperType,
        //        PaperGramWeight = orderRoll.Roll?.PaperGramWeight,
        //        PaperWidth = orderRoll.Roll?.PaperWidth
        //    };
        //}


        //public async Task<OrderRollResponseDto> CreateOrderRollAsync(OrderRollCreateDto dto)
        //{
        //    var existingOrderRoll = await _orderRollRepository.AddAsync(dto.OrderId, dto.RollId);

        //    var newOrderRoll = new OrderRoll
        //    {
        //        Id = Guid.NewGuid(),
        //        OrderId = dto.OrderId,
        //        RollId = dto.RollId,
        //        IntakeWeight = dto.NewRollWeight
        //    };

        //    if (existingOrderRoll != null)
        //    {
        //        newOrderRoll.IntakeWeight += existingOrderRoll.IntakeWeight;
        //    }

        //    await _orderRollRepository.CreateOrderRollAsync(newOrderRoll);

        //    return new OrderRollResponseDto
        //    {
        //        Id = newOrderRoll.Id,
        //        OrderId = newOrderRoll.OrderId,
        //        RollId = newOrderRoll.RollId,
        //        IntakeWeight = newOrderRoll.IntakeWeight,
        //        OutputWeight = newOrderRoll.OutputWeight,
        //        ConsumedWeight = newOrderRoll.ConsumedWeight,

        //        RollNumber = newOrderRoll.Roll?.RollNumber,
        //        PaperType = newOrderRoll.Roll?.PaperType,
        //        PaperGramWeight = newOrderRoll.Roll?.PaperGramWeight,
        //        PaperWidth = newOrderRoll.Roll?.PaperWidth
        //    };
        //}

        //public Task<OrderRollResponseDto?> UpdateOrderRollAsync(OrderRollUpdateDto dto)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<bool> DeleteOrderRollAsync(Guid id)
        //{
        //    throw new NotImplementedException();
        //}

    }
}
