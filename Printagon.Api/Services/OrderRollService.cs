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
            var orderRolls = await _orderRollRepository.GetOrderRollsByOrderIdAsync(orderId);

            return orderRolls.Select(or => new OrderRollResponseDto
            {
                Id = or.Id,
                OrderId = or.OrderId,
                RollId = or.RollId,
                IntakeWeight = or.IntakeWeight,
                OutputWeight = or.OutputWeight,
                ConsumedWeight = or.ConsumedWeight,

                RollNumber = or.Roll?.RollNumber,
                PaperType = or.Roll?.PaperType,
                PaperGramWeight = or.Roll?.PaperGramWeight,
                PaperWidth = or.Roll?.PaperWidth
            });
        }


        public Task<OrderRollResponseDto?> GetByOrderAndRollAsync(Guid orderId, Guid rollId)
        {
            throw new NotImplementedException();
        }
        public Task<OrderRollResponseDto?> UpdateAsync(OrderRollUpdateDto dto)
        {
            throw new NotImplementedException();
        }
        public Task<OrderRollResponseDto> CreateAsync(OrderRollCreateDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

    }
}
