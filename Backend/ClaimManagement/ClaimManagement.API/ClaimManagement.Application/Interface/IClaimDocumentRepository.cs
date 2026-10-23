using ClaimManagement.Application.DTO;
using ClaimManagement.Domain.Entities;

namespace ClaimManagement.Application.Interface
{
    public interface IClaimDocumentRepository
    {
        Task<ClaimDocumentResponseDto> AddDocumentAsync(int claimId, CreateClaimDocumentDto dto);
        Task<List<ClaimDocumentResponseDto>> GetDocumentsByClaimIdAsync(int claimId);
        Task<List<ClaimDocumentResponseDto>?> VerifyDocumentsAsync(int claimId, List<int> documentIds);
    }
}