using Printagon.Api.DTOs.Roll;
using Printagon.Api.Models;

namespace Printagon.Api.Services.Interfaces
{
    public interface IRollService
    {
        Task<IEnumerable<RollResponseDto>> GetAllRollsAsync();
        Task<RollResponseDto?> GetRollByIdAsync(Guid rollId);
        Task<RollResponseDto> CreateRollAsync(Guid orerId, RollCreateDto createRoll);
        Task<RollResponseDto?> UpdateRollAsync(Guid orderId, Guid rollId, RollUpdateDto updatedRoll);
        Task<bool?> DeleteRollAsync(Guid rollId);
    }
}
