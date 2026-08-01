
using Printagon.Api.Models;

namespace Printagon.Api.Services.Interfaces
{
    public interface IRollService
    {
        Task<IEnumerable<RollResponseDto>> GetAllRollsAsync();
        Task<RollResponseDto?> GetRollByNumberAsync(int rollNumber);
        Task<RollResponseDto> CreateRollAsync(int orderNumber, RollCreateDto createRoll);
        Task<RollResponseDto?> UpdateRollAsync(int orderNumber, int rollNumber, RollUpdateDto updatedRoll);
        Task DeleteRollAsync(int rollNumber);
    }
}
