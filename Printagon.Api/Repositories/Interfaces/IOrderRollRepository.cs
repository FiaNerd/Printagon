using Printagon.Api.Models;

namespace Printagon.Api.Repositories.Interfaces
{
    public interface IOrderRollRepository
    {
        Task<List<OrderRoll>> GetByOrderIdAsync(Guid orderId);

        Task<OrderRoll?> GetByIdAsync(Guid orderRollId);

        Task<OrderRoll?> GetByOrderAndRollAsync(Guid orderId, Guid rollId);

        Task AddAsync(OrderRoll orderRoll);

        void Update(OrderRoll orderRoll);

        void Remove(OrderRoll orderRoll);

        Task SaveChangesAsync();
    }
}