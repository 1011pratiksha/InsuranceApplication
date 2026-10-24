using ClaimManagement.Application.DTO;

namespace ClaimManagement.Application.Interface
{
    public interface IClaimRepository
    {
        Task<ClaimResponseDto> AddClaimAsync(CreateClaimDto dto);
        Task<List<ClaimResponseDto>> GetAllClaimsAsync();
        Task<ClaimResponseDto?> GetClaimByIdAsync(long claimId);
        Task<string> UpdateClaimAsync(long claimId, UpdateClaimDto dto);
        Task<string> DeleteClaimAsync(long claimId);
    }
}