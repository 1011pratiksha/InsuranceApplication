using ClaimManagement.Application.DTO;

namespace ClaimManagement.Application.Interface
{
    public interface IClaimEligibilityRepository
    {
        Task<string> CheckEligibilityAsync(CreateClaimDto dto);
    }
}