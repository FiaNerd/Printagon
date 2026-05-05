using Printagon.Api.DTOs.OrderRoll;

public interface IOrderRollService
{
    Task<IEnumerable<OrderRollResponseDto>> GetAllByOrderIdAsync(Guid orderId);
    Task<OrderRollResponseDto?> GetOrderAndRollByIdAsync(Guid orderId, Guid rollId);
    Task<OrderRollResponseDto> CreateAsync(OrderRollCreateDto dto);
    Task<OrderRollResponseDto?> UpdateAsync(OrderRollUpdateDto dto);
    Task<bool> DeleteAsync(Guid id);
}
