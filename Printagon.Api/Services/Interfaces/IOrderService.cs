using Printagon.Api.DTOs.Order;

public interface IOrderService
{
    Task<OrderResponseDto?> GetOrderByIdAsync(Guid orderId);
    Task<IEnumerable<OrderResponseDto>> GetAllOrdersAsync();
    Task<OrderResponseDto> CreateOrderAsync(OrderCreateDto orderCreateDto);
    Task<OrderResponseDto?> UpdateOrderAsync(Guid orderId, OrderUpdateDto orderUpdateDto);
    Task<bool> DeleteOrderAsync(Guid orderId);
}