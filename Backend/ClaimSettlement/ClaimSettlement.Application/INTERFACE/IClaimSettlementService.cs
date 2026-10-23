
using ClaimSettlement.Application.DTO;

namespace ClaimSettlement.Application.Interface
{
    public interface IClaimSettlementService
    {
        Task<SettlementResponseDTO> ProcessSettlement(long claimId,SettlementDTO dto);

        Task<SettlementResponseDTO?> FindSettlementByClaimId(long claimId);

        Task<string> ApproveClaim(long claimId);

        Task<string> RejectClaim(long claimId);
    }
}
