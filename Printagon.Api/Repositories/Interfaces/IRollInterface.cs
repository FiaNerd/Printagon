using Printagon.Api.Models;

namespace Printagon.Api.Repositories.Interfaces
{
    public interface IRollInterface
    {
        Task<Roll?> GetRollByIdAsync(int rollId);
        Task<IEnumerable<Roll>> GetAllRollsAsync();
        Task<Roll> CreateRollAsync(Roll roll);
        Task<Roll?> UpdateRollAsync(int rollId, Roll updatedRoll);
        Task<bool> DeleteRollAsync(int rollId);
    }
}
