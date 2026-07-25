namespace Printagon.Api.Repositories.Interfaces
{
    public interface IOrderRollRepository
    {
        Task<IEnumerable<OrderRoll>> GetAllByOrderIdAsync(Guid orderId);

        Task<OrderRoll?> GetByIdAsync(Guid orderRollId);

        Task<OrderRoll?> GetByOrderAndRollAsync(Guid orderId, Guid rollId);

        Task<OrderRoll> AddAsync(OrderRoll orderRoll);

        Task UpdateAsync(OrderRoll orderRoll);

        Task DeleteAsync(OrderRoll orderRoll);

        Task SaveChangesAsync();
    }
}