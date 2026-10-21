namespace ClaimManagement.Application.Interface
{
    public interface IClaimEligibilityRepository
    {
        Task<bool> CustomerExistsAsync(long customerId);

        Task<bool> PolicyBelongsToCustomerAsync(long policyId, long customerId);

        Task<bool> IsPolicyActiveAsync(long policyId, DateTime incidentDate);

        Task<bool> HasValidCoverageAsync(long policyId, DateTime incidentDate, decimal estimatedLoss);

        Task<bool> HasEligiblePremiumAsync(long policyId);
    }
}