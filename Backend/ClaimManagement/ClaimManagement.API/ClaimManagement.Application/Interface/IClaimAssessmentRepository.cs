using ClaimManagement.Application.DTO;

namespace ClaimManagement.Application.Interface
{
    public interface IClaimAssessmentRepository
    {
        Task<AssessmentResponseDto> AddAssessmentAsync(long claimId, CreateAssessmentDto dto);
        Task<AssessmentResponseDto?> GetAssessmentByClaimIdAsync(long claimId);
    }
}
