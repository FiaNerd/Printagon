using Printagon.Api.Models;

namespace Printagon.Api.Repositories.Interfaces
{
    public interface IOrderRollRepository
    {
        Task<IEnumerable<OrderRoll>> GetOrderRollsByOrderIdAsync(Guid orderId);
        Task<OrderRoll?> GetOrderRollByIdAsync(Guid orderRollId);
        Task<OrderRoll?> GetOrderRollByOrderIdAndRollIdAsync(Guid orderId, Guid rollId);
        Task<OrderRoll> CreateOrderRollAsync(OrderRoll newOrderRoll);
        Task<OrderRoll?> UpdateOrderRollAsync(OrderRoll updateOrderRoll);
        Task<bool> DeleteOrderRollByIdAsync(Guid orderRollId);
    }
}