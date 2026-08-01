using Printagon.Api.Models;

namespace Printagon.Api.Repositories.Interfaces
{
    public interface IRollRepository
    {
        Task<IEnumerable<Roll>> GetAllRollsAsync();
        Task<Roll?> GetRollByNumberAsync(int rollNumber);
        Task<Roll> CreateRollAsync(Roll roll);
        Task<Roll?> UpdateRollAsync(Roll updatedRoll);
        Task<bool> DeleteRollAsync(int rollNumber);
    }
}
