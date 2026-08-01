using Printagon.Api.DTOs.OrderRoll;

public interface IOrderRollService
{
    Task<IEnumerable<OrderRollResponseDto>> GetAllByOrderNumberAsync(int orderNumber);
    Task<OrderRollResponseDto?> GetByIdAsync(Guid id);
    Task<OrderRollResponseDto?> GetByOrderAndRollAsync(int orderNumber, int rollNumber);

    Task<OrderRollResponseDto> AddAsync(int orderNumber, OrderRollCreateDto dto);
    Task<OrderRollResponseDto?> UpdateAsync(Guid id, OrderRollUpdateDto dto);
    Task<bool> DeleteAsync(Guid id);
}
