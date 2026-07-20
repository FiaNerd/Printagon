using Printagon.Api.DTOs.OrderRoll;

public interface IOrderRollService
{
    Task<IEnumerable<OrderRollResponseDto>> GetAllByOrderIdAsync(Guid orderId);

    Task<OrderRollResponseDto?> GetByIdAsync(Guid orderRollId);

    Task<OrderRollResponseDto?> GetByOrderAndRollAsync(Guid orderId, Guid rollId);

    Task<OrderRollResponseDto> AddAsync(OrderRollCreateDto dto);

    Task<OrderRollResponseDto?> UpdateAsync(Guid id, OrderRollUpdateDto dto);

    Task<bool> RemoveAsync(Guid id);
}
