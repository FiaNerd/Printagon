using Printagon.Api.DTOs.Order;

namespace Printagon.Api.Services
{
    public class OrderService : IOrderService
    {
        public Task<OrderResponseDto?> GetOrderByIdAsync(Guid orderId)
        {
            throw new NotImplementedException();
        }


        public Task<IEnumerable<OrderResponseDto>> GetAllOrdersAsync()
        {
            throw new NotImplementedException();
        }


        public Task<OrderResponseDto> CreateOrderAsync(OrderCreateDto orderCreateDto)
        {
            throw new NotImplementedException();
        }

        public Task<OrderResponseDto?> UpdateOrderAsync(Guid orderId, OrderUpdateDto orderUpdateDto)
        {
            throw new NotImplementedException();
        }


        public Task<bool> DeleteOrderAsync(Guid orderId)
        {
            throw new NotImplementedException();
        }
    }
}
