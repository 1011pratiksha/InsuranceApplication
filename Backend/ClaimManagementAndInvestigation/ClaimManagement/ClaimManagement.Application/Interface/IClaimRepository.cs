using ClaimManagement.Domain.Entities;

namespace ClaimManagement.Application.Interface
{
    public interface IClaimRepository
    {
        Task<Claim> AddAsync(Claim claim);
        Task<List<Claim>> GetAllAsync();
        Task<Claim?> GetByIdAsync(long claimId);
        Task<bool> ExistsAsync(long claimId);
        Task UpdateAsync(Claim claim);
        Task<bool> DeleteAsync(long claimId);
    }
}

