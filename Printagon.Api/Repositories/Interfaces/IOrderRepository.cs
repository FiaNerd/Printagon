using Printagon.Api.Models;

namespace Printagon.Api.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<Order?> GetOrderByNumberAsync(int orderNumber);
        Task<Order> CreateOrderByNumberAsync(Order order);
        Task<Order?> UpdateOrderAsync(int orderNumber, Order order);
        Task<bool> DeleteOrderAsync(int OrderNumber);
    }
}
