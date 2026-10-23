using ClaimManagement.Application.DTO;
using ClaimManagement.Application.Interface;
using ClaimManagement.Domain.Entities;
using ClaimManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using SettlementEntity = ClaimManagement.Domain.Entities.ClaimSettlement;

namespace ClaimManagement.Infrastructure.Repository
{
    public class ClaimSettlementRepository : IClaimSettlementRepository
    {
        AppDbContext dbcontext;

        public ClaimSettlementRepository(AppDbContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        public async Task<SettlementResponseDTO> ProcessSettlement(long claimId, SettlementDTO dto)
        {
            var claim = await dbcontext.ClaimMasters.FirstOrDefaultAsync(x => x.claim_id == claimId);

            if (claim == null)
                throw new KeyNotFoundException("Claim not found.");

            if (claim.claim_status != "Approved")
                throw new InvalidOperationException(
                    "Only approved claims can be settled.");

            var existing = await dbcontext.ClaimSettlements.FirstOrDefaultAsync(x => x.claim_id == claimId);

            if (existing != null)
                throw new InvalidOperationException(
                    "Settlement already exists for this claim.");

            var assessment = await dbcontext.ClaimAssessments.FirstOrDefaultAsync(x => x.claim_id == claimId);

            if (assessment == null)
                throw new KeyNotFoundException(
                    "Claim assessment not found.");

            if (assessment.net_payable <= 0)
                throw new InvalidOperationException(
                    "Net payable amount must be greater than zero.");

            var allowedModes = new[] { "NEFT", "RTGS", "IMPS", "UPI", "Cheque" };

            if (!allowedModes.Contains(dto.PaymentMode, StringComparer.OrdinalIgnoreCase))
                throw new ArgumentException("Invalid payment mode.");

            var settlement = new SettlementEntity
            {
                claim_id = claimId,
                approved_amount = assessment.net_payable,
                payment_mode = dto.PaymentMode,
                transaction_reference = $"TXN-{claimId}-{DateTime.UtcNow:yyyyMMddHHmmss}",
                settlement_date = DateTime.UtcNow,
                settlement_status = "Settled"
            };

            await dbcontext.ClaimSettlements.AddAsync(settlement);

            claim.claim_status = "Settled";

            await dbcontext.SaveChangesAsync();

            return ConvertToDTO(settlement);
        }

        public async Task<SettlementResponseDTO?> FindSettlementByClaimId(long claimId)
        {
            var settlement = await dbcontext.ClaimSettlements.FirstOrDefaultAsync(x => x.claim_id == claimId);

            if (settlement == null)
                return null;

            return ConvertToDTO(settlement);
        }

        private static SettlementResponseDTO ConvertToDTO(SettlementEntity settlement)
        {
            return new SettlementResponseDTO
            {
                SettlementId = settlement.settlement_id,
                ClaimId = settlement.claim_id,
                ApprovedAmount = settlement.approved_amount,
                PaymentMode = settlement.payment_mode,
                TransactionReference = settlement.transaction_reference,
                SettlementDate = settlement.settlement_date,
                SettlementStatus = settlement.settlement_status
            };
        }

        public async Task<string> ApproveClaim(long claimId)
        {
            var claim = await dbcontext.ClaimMasters.FirstOrDefaultAsync(c => c.claim_id == claimId);

            if (claim == null)
                throw new KeyNotFoundException("Claim not found.");

            if (claim.claim_status == "Rejected")
                throw new InvalidOperationException(
                    "A rejected claim cannot be approved.");

            if (claim.claim_status == "Settled")
                throw new InvalidOperationException(
                    "A settled claim cannot be approved again.");

            if (claim.claim_status == "Approved")
                throw new InvalidOperationException(
                    "Claim is already approved.");

            claim.claim_status = "Approved";

            await dbcontext.SaveChangesAsync();

            return "Claim approved successfully.";
        }

        public async Task<string> RejectClaim(long claimId)
        {
            var claim = await dbcontext.ClaimMasters.FirstOrDefaultAsync(c => c.claim_id == claimId);

            if (claim == null)
                throw new KeyNotFoundException("Claim not found.");

            if (claim.claim_status == "Settled")
                throw new InvalidOperationException(
                    "A settled claim cannot be rejected.");

            if (claim.claim_status == "Rejected")
                throw new InvalidOperationException(
                    "Claim is already rejected.");

            var existingSettlement = await dbcontext.ClaimSettlements.AnyAsync(s => s.claim_id == claimId);

            if (existingSettlement)
                throw new InvalidOperationException(
                    "A claim with an existing settlement cannot be rejected.");

            claim.claim_status = "Rejected";
            await dbcontext.SaveChangesAsync();
            return "Claim rejected successfully.";
        }
    }
}