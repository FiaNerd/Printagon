using Printagon.Api.Models;

namespace Printagon.Api.Repositories.Interfaces
{
    public interface IOrderRollRepository
    {
        Task<IEnumerable<OrderRoll>> GetAllByOrderNumberAsync(int orderNumber);

        Task<OrderRoll?> GetByIdAsync(Guid id);

        Task<OrderRoll?> GetByOrderAndRollAsync(int orderNumber, int rollNumber);

        Task<OrderRoll> AddAsync(OrderRoll orderRoll);

        Task<bool> UpdateAsync(OrderRoll orderRoll);

        Task<bool> DeleteAsync(OrderRoll orderRoll);

        Task SaveChangesAsync();
    }
}
