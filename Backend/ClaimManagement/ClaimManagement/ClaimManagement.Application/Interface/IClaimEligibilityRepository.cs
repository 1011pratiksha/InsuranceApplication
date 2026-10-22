using ClaimManagement.Application.DTO;

namespace ClaimManagement.Application.Interface
{
    public interface IClaimEligibilityRepository
    {
        // Returns "" if the claim is eligible, otherwise returns the error message
        Task<string> CheckEligibilityAsync(CreateClaimDto dto);
    }
}