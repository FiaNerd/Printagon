using Printagon.Api.DTOs.Order;

public interface IOrderService
{
    Task<OrderResponseDto?> GetOrderByIdAsync(int orderNumber);
    Task<IEnumerable<OrderResponseDto>> GetAllOrdersAsync();
    Task<OrderResponseDto> CreateOrderAsync(OrderCreateDto orderCreateDto);
    Task<OrderResponseDto?> UpdateOrderAsync(int orderNumber, OrderUpdateDto orderUpdateDto);
    Task<bool> DeleteOrderAsync(int orderNumber);
}