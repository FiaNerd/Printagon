using Printagon.Api.DTOs.OrderRoll;

namespace Printagon.Api.Services.Interfaces
{
    public interface IOrderRoll
    {
        Task<IEnumerable<OrderRollDto>> GetAllOrderRollsAsync();
        Task<OrderRollDto?> GetOrderRollByIdAsync(Guid orderRollId);
        Task<OrderRollDto> CreateOrderRollAsync(OrderRollCreateDto createOrderRoll);
        Task<OrderRollDto?> UpdateOrderRollAsync(Guid orderRollId, OrderRollUpdateDto updatedOrderRoll);
        Task DeleteOrderRollAsync(Guid orderRollId);
    }
}
