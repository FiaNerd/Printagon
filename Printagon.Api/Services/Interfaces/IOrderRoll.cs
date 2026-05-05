using Printagon.Api.DTOs.OrderRoll;

namespace Printagon.Api.Services.Interfaces
{
    public interface IOrderRoll
    {
        Task<IEnumerable<OrderRollDto>> GetOrderRollsByOrderIdAsync(Guid orderId);
        Task<OrderRollDto?> GetOrderRollByIdAsync(Guid orderRollId);
        Task<OrderRollDto?> GetOrderRollByOrderIdAndRollIdAsync(Guid orderId, Guid rollId);
        Task<OrderRollCreateDto> CreateOrderRollAsync(OrderRollCreateDto newOrderRoll);
        Task<OrderRollUpdateDto?> UpdateOrderRollAsync(OrderRollUpdateDto updateOrderRoll);
        Task<bool> DeleteOrderRollByIdAsync(Guid orderRollId);
    }
}
