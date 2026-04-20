using Printagon.Api.Models;

namespace Printagon.Api.Repositories.Interfaces
{
    public interface IOrderRollRepository
    {
        Task<OrderRoll?> GetByOrderAndRollAsync(Guid orderId, Guid rollId);
        Task<IEnumerable<OrderRoll>> GetRollsForOrderAsync(Guid orderId);
        Task<OrderRoll> CreateOrderRollAsync(OrderRoll orderRoll);
        Task<OrderRoll?> UpdateOrderRollAsync(OrderRoll orderRoll);
        Task DeleteOrderRollAsync(OrderRoll orderRoll);
    }
}
