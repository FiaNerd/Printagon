using Printagon.Api.DTOs.OrderRoll;

public interface IOrderRollService
{
    Task<IEnumerable<OrderRollResponseDto>> GetAllByOrderIdAsync(Guid orderId);
    Task<OrderRollResponseDto?> GetOrderAndRollByIdAsync(Guid orderId, Guid rollId);
    Task<OrderRollResponseDto> CreateOrderRollAsync(OrderRollCreateDto dto);
    Task<OrderRollResponseDto?> UpdateOrderRollAsync(OrderRollUpdateDto dto);
    Task<bool> DeleteOrderRollAsync(Guid id);
}
