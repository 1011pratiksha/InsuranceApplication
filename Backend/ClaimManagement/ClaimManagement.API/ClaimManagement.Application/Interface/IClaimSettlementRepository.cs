using ClaimManagement.Application.DTO;

namespace ClaimManagement.Application.Interface
{
    public interface IClaimSettlementRepository
    {
        Task<SettlementResponseDTO> ProcessSettlement(long claimId,SettlementDTO dto);

        Task<SettlementResponseDTO?> FindSettlementByClaimId(long claimId);

        Task<string> ApproveClaim(long claimId);

        Task<string> RejectClaim(long claimId);
    }
}
