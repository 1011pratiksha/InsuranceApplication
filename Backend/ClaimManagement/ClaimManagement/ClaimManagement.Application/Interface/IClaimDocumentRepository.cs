using ClaimManagement.Domain.Entities;

namespace ClaimManagement.Application.Interface
{
    public interface IClaimDocumentRepository
    {
        Task<ClaimDocument> AddAsync(ClaimDocument document);
        Task<List<ClaimDocument>> GetByClaimIdAsync(int claimId);
        Task<ClaimDocument?> GetByIdAsync(int documentId);
        Task UpdateAsync(ClaimDocument document);
    }
}