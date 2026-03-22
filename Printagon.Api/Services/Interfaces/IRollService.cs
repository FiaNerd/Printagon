using Printagon.Api.DTOs.Roll;
using Printagon.Api.Models;

namespace Printagon.Api.Services.Interfaces
{
    public interface IRollService
    {
        Task<IEnumerable<RollResponseDto>> GetAllRollsAsync();
        Task<RollResponseDto?> GetRollByIdAsync(Guid rollId);
        Task<Roll> CreateRollAsync(Roll roll);
        Task<Roll?> UpdateRollAsync(Guid rollId, Roll updatedRoll);
        Task<bool?> DeleteRollAsync(Guid rollId);
    }
}
