using Printagon.Api.Models;

namespace Printagon.Api.Repositories.Interfaces
{
    public interface IRollRepository
    {
        Task<IEnumerable<Roll>> GetAllRollsAsync();
        Task<Roll?> GetRollByIdAsync(Guid rollId);
        Task<Roll> CreateRollAsync(Roll roll);
        Task<Roll?> UpdateRollAsync(Roll updatedRoll);
        Task<bool> DeleteRollAsync(Guid rollId);
    }
}

