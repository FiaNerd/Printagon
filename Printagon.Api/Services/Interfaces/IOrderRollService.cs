using Printagon.Api.DTOs.OrderRoll;

public interface IOrderRollService
{
    Task<OrderRollDto?> GetByIdAsync(Guid id);
    Task<OrderRollDto?> GetByOrderAndRollAsync(Guid orderId, Guid rollId);
    Task<IEnumerable<OrderRollDto>> GetByOrderIdAsync(Guid orderId);

    Task<OrderRollDto> CreateAsync(OrderRollCreateDto dto);
    Task<OrderRollDto?> UpdateAsync(OrderRollUpdateDto dto);
    Task<bool> DeleteAsync(Guid id);
}
