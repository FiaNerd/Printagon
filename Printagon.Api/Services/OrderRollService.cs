using Printagon.Api.DTOs.OrderRoll;
using Printagon.Api.Repositories.Interfaces;
using Printagon.Api.Services.Interfaces;

namespace Printagon.Api.Services
{
    public class OrderRollService : IOrderRollService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderRollService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public Task<OrderRollDto> CreateAsync(OrderRollCreateDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<OrderRollDto?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<OrderRollDto?> GetByOrderAndRollAsync(Guid orderId, Guid rollId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OrderRollDto>> GetByOrderIdAsync(Guid orderId)
        {
            throw new NotImplementedException();
        }

        public Task<OrderRollDto?> UpdateAsync(OrderRollUpdateDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
