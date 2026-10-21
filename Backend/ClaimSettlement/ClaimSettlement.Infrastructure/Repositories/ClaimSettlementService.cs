using ClaimSettlement.Application.DTO;
using ClaimSettlement.Application.Interface;
using ClaimSettlement.Domain.Entities;
using ClaimSettlement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using SettlementEntity = ClaimSettlement.Domain.Entities.ClaimSettlement;


namespace ClaimSettlement.Infrastructure.Repositories;

public class ClaimSettlementService : IClaimSettlementService
{
    AppDbContext dbcontext;

    public ClaimSettlementService(AppDbContext dbcontext)
    {
        this.dbcontext = dbcontext;
    }

    public async Task<SettlementResponseDTO> ProcessSettlement(long claimId, SettlementDTO dto)
    {
        var claim = await dbcontext.ClaimMasters.FirstOrDefaultAsync(x => x.ClaimId == claimId);

        if (claim == null)
            throw new KeyNotFoundException("Claim not found.");

        if (claim.ClaimStatus != "Approved")
            throw new InvalidOperationException(
                "Only approved claims can be settled.");

        var existing = await dbcontext.ClaimSettlements.FirstOrDefaultAsync(x => x.ClaimId == claimId);

        if (existing != null)
            throw new InvalidOperationException(
                "Settlement already exists for this claim.");

        var policy = await dbcontext.Policies.FirstOrDefaultAsync(x => x.PolicyId == claim.PolicyId);

        if (policy == null)
            throw new KeyNotFoundException("Policy not found.");

        if (policy.PolicyStatus != "Active")
            throw new InvalidOperationException(
                "Policy is not active.");

        if (policy.PolicyExpiryDate.Date < DateTime.UtcNow.Date)
            throw new InvalidOperationException(
                "Policy has expired.");

        var customer = await dbcontext.Customers.FirstOrDefaultAsync(x => x.CustomerId == claim.CustomerId);

        if (customer == null)
            throw new KeyNotFoundException("Customer not found.");

        if (customer.CustomerStatus != "Active")
            throw new InvalidOperationException(
                "Customer is inactive.");

        var assessment = await dbcontext.ClaimAssessments.FirstOrDefaultAsync(x => x.ClaimId == claimId);

        if (assessment == null)
            throw new KeyNotFoundException(
                "Claim assessment not found.");

        if (assessment.NetPayable <= 0)
            throw new InvalidOperationException(
                "Net payable amount must be greater than zero.");

        if (assessment.NetPayable > policy.SumInsured)
            throw new InvalidOperationException(
                "Settlement amount exceeds sum insured.");

        bool bankVerified = true;

        if (!bankVerified)
            throw new InvalidOperationException(
                "Bank verification failed.");

        var allowedModes = new[]
        {
            "NEFT", "RTGS", "IMPS", "UPI", "Cheque"
        };

        if (!allowedModes.Contains(dto.PaymentMode,StringComparer.OrdinalIgnoreCase))
        {
            throw new ArgumentException("Invalid payment mode.");
        }

        var settlement = new SettlementEntity
        {
            ClaimId = claimId,
            ApprovedAmount = assessment.NetPayable,
            PaymentMode = dto.PaymentMode,
            TransactionReference = null,
            SettlementDate = null,
            SettlementStatus = "Pending"
        };

        await dbcontext.ClaimSettlements.AddAsync(settlement);
        await dbcontext.SaveChangesAsync();

        return ConvertToDTO(settlement);
    }

    public async Task<SettlementResponseDTO?> FindSettlementByClaimId(long claimId)
    {
        var settlement = await dbcontext.ClaimSettlements.FirstOrDefaultAsync(x => x.ClaimId == claimId);

        if (settlement == null)
            return null;

        return ConvertToDTO(settlement);
    }

    private static SettlementResponseDTO ConvertToDTO(SettlementEntity settlement)
    {
        return new SettlementResponseDTO
        {
            SettlementId = settlement.SettlementId,
            ClaimId = settlement.ClaimId,
            ApprovedAmount = settlement.ApprovedAmount,
            PaymentMode = settlement.PaymentMode,
            TransactionReference = settlement.TransactionReference,
            SettlementDate = settlement.SettlementDate,
            SettlementStatus = settlement.SettlementStatus
        };
    }


    public async Task<string> ApproveClaim(long claimId)
    {
        var claim = await dbcontext.ClaimMasters.FirstOrDefaultAsync(c => c.ClaimId == claimId);

        if (claim == null)
            throw new KeyNotFoundException("Claim not found.");

        if (claim.ClaimStatus == "Rejected")
            throw new InvalidOperationException(
                "A rejected claim cannot be approved.");

        if (claim.ClaimStatus == "Settled")
            throw new InvalidOperationException(
                "A settled claim cannot be approved again.");

        if (claim.ClaimStatus == "Approved")
            throw new InvalidOperationException(
                "Claim is already approved.");

        claim.ClaimStatus = "Approved";

        await dbcontext.SaveChangesAsync();

        return "Claim approved successfully.";
    }

    public async Task<string> RejectClaim(long claimId)
    {
        var claim = await dbcontext.ClaimMasters.FirstOrDefaultAsync(c => c.ClaimId == claimId);

        if (claim == null)
            throw new KeyNotFoundException("Claim not found.");

        if (claim.ClaimStatus == "Settled")
            throw new InvalidOperationException(
                "A settled claim cannot be rejected.");

        if (claim.ClaimStatus == "Rejected")
            throw new InvalidOperationException(
                "Claim is already rejected.");

        var existingSettlement = await dbcontext.ClaimSettlements.AnyAsync(s => s.ClaimId == claimId);

        if (existingSettlement)
            throw new InvalidOperationException(
                "A claim with an existing settlement cannot be rejected.");

        claim.ClaimStatus = "Rejected";
        await dbcontext.SaveChangesAsync();
        return "Claim rejected successfully.";
    }

}