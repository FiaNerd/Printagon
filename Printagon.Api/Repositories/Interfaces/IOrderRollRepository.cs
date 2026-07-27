namespace Printagon.Api.Repositories.Interfaces
{
    public interface IOrderRollRepository
    {
        Task<IEnumerable<OrderRoll>> GetAllByOrderIdAsync(Guid orderId);

        Task<OrderRoll?> GetByIdAsync(Guid orderRollId);

        Task<OrderRoll?> GetByOrderAndRollAsync(Guid orderId, Guid rollId);

        Task<OrderRoll> AddAsync(OrderRoll orderRoll);

        Task<bool> UpdateAsync(OrderRoll orderRoll);

        Task<bool> DeleteAsync(OrderRoll orderRoll);

        Task SaveChangesAsync();
    }
}