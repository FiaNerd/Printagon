using Printagon.Api.Models;

namespace Printagon.Api.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order?> GetOrderByIdAsync(Guid orderId);
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<Order> CreateOrderAsync(Order order);
        Task<Order?> UpdateOrderAsync(Guid orderId, Order order);
        Task<bool> DeleteOrderAsync(Guid orderId);
    }
}
