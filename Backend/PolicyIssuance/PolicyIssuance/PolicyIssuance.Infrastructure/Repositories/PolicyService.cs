using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PolicyIssuance.Application.DTO;
using PolicyIssuance.Application.Interfaces;
using PolicyIssuance.Domain.Entities;
using PolicyIssuance.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolicyIssuance.Infrastructure.Repositories
{
    public class PolicyService : IPolicyService
    {
        AppDbContext dbcontext;
        IMapper mapper;
        IPolicyIntegrationService integration;

        public PolicyService(AppDbContext dbcontext, IMapper mapper, IPolicyIntegrationService integration)
        {
            this.dbcontext = dbcontext;
            this.mapper = mapper;
            this.integration = integration;

        }

        public async Task<PolicyDetailDto> IssuePolicyAsync(
      IssuePolicyDto dto)
        {
            // 1. Get customer from Customer Registration API
            var customer = await integration.GetCustomerAsync(
                dto.CustomerId);

            if (customer == null)
                throw new InvalidOperationException("Customer not found.");

            // 2. Verify KYC through KYC API
            var kyc = await integration.GetKycAsync(
                dto.CustomerId);

            if (kyc == null ||
                !string.Equals(
                    kyc.Status, "Verified",
                    StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException(
                    "Customer KYC is not verified.");
            }

            // 3. Get product from Product Management API
            var product = await integration.GetProductAsync(
                dto.ProductId);

            if (product == null ||
                !string.Equals(
                    product.Status, "Active",
                    StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException(
                    "Insurance product is not active.");
            }

            // 4. Get quote from Quote Generation API
            var quote = await integration.GetQuoteAsync(
                dto.QuoteId);

            if (quote == null)
                throw new InvalidOperationException("Quote not found.");

            if (quote.CustomerId != dto.CustomerId ||
                quote.ProductId != dto.ProductId)
            {
                throw new InvalidOperationException(
                    "Quote does not match the customer and product.");
            }

            if (!string.Equals(
                quote.Status, "Approved",
                StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException(
                    "Quote is not approved.");
            }

            // 5. Check underwriting approval
            var underwriting = await integration.GetUnderwritingAsync(
                dto.QuoteId);

            if (underwriting == null ||
                !string.Equals(
                    underwriting.Decision, "Approved",
                    StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException(
                    "Underwriting approval is required.");
            }

            // 6. Verify first premium payment
            var payment = await integration.GetPaymentAsync(
                dto.QuoteId);

            if (payment == null ||
                !string.Equals(
                    payment.Status, "Successful",
                    StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException(
                    "Successful first premium payment is required.");
            }

            // 7. Prevent duplicate policy creation for the same quote
            bool alreadyIssued = await dbcontext.Policies
                .AnyAsync(p => p.QuoteId == dto.QuoteId);

            if (alreadyIssued)
                throw new InvalidOperationException(
                    "A policy already exists for this quote.");

            // 8. Create policy using only Policy Issuance entities
            var policy = new Policy
            {
                PolicyNumber =
                    $"POL-{DateTime.UtcNow:yyyy}-{Guid.NewGuid().ToString("N")[..8].ToUpperInvariant()}",

                QuoteId = dto.QuoteId,
                CustomerId = dto.CustomerId,
                ProductId = dto.ProductId,
                IssueDate = DateTime.UtcNow,
                EffectiveDate = dto.EffectiveDate,
                ExpiryDate = dto.ExpiryDate,

                // Change to Pending if your workflow activates separately.
                PolicyStatus = "Active"
            };

            if (dto.Coverages != null && dto.Coverages.Count > 0)
            {
                policy.PolicyCoverages =
                    mapper.Map<List<PolicyCoverage>>(dto.Coverages);
            }

            if (dto.Riders != null && dto.Riders.Count > 0)
            {
                policy.PolicyRiders =
                    mapper.Map<List<PolicyRider>>(dto.Riders);
            }

            policy.PolicyHistories.Add(new PolicyHistory
            {
                Action = "POLICY_ISSUED",
                ActionDate = DateTime.UtcNow,
                Remarks = "Policy issued after prerequisite validations."
            });

            dbcontext.Policies.Add(policy);
            await dbcontext.SaveChangesAsync();

            // 9. Register document and send notification
            await integration.RegisterPolicyDocumentAsync(
                policy.PolicyId);

            await integration.NotifyPolicyIssuedAsync(
                policy.PolicyId);

            return mapper.Map<PolicyDetailDto>(policy);
        }

        public async Task<List<PolicyDto>> GetAllPoliciesAsync()
        {
            var policies = await dbcontext.Policies.ToListAsync();
            return mapper.Map<List<PolicyDto>>(policies);
        }

        public async Task<PolicyDetailDto?> GetPolicyByIdAsync(int id)
        {
            var policy = await dbcontext.Policies
                .Include(p => p.PolicyCoverages)
                .Include(p => p.PolicyRiders)
                .Include(p => p.PolicyDocuments)
                .Include(p => p.PolicyEndorsements)
                .Include(p => p.PolicyHistories)
                .FirstOrDefaultAsync(p => p.PolicyId == id);

            if (policy == null)
            {
                return null;
            }

            return mapper.Map<PolicyDetailDto>(policy);
        }

        public async Task<PolicyDetailDto?> UpdatePolicyAsync(int id, UpdatePolicyDto dto)
        {
            var policy = await dbcontext.Policies
                .Include(p => p.PolicyCoverages)
                .Include(p => p.PolicyRiders)
                .Include(p => p.PolicyDocuments)
                .Include(p => p.PolicyEndorsements)
                .Include(p => p.PolicyHistories)
                .FirstOrDefaultAsync(p => p.PolicyId == id);

            if (policy == null)
            {
                return null;
            }

            if (dto.EffectiveDate.HasValue)
            {
                policy.EffectiveDate = dto.EffectiveDate.Value;
            }

            if (dto.ExpiryDate.HasValue)
            {
                policy.ExpiryDate = dto.ExpiryDate.Value;
            }

            if (!string.IsNullOrEmpty(dto.PolicyStatus))
            {
                policy.PolicyStatus = dto.PolicyStatus;
            }

            policy.PolicyHistories.Add(new PolicyHistory
            {
                Action = "POLICY_UPDATED",
                ActionDate = DateTime.UtcNow,
                Remarks = "Policy details updated"
            });

            await dbcontext.SaveChangesAsync();
            return mapper.Map<PolicyDetailDto>(policy);
        }

        public async Task<bool> DeletePolicyAsync(int id)
        {
            var policy = await dbcontext.Policies.FindAsync(id);

            if (policy == null)
            {
                return false;
            }

            policy.PolicyStatus = "Cancelled";

            dbcontext.PolicyHistories.Add(new PolicyHistory
            {
                PolicyId = id,
                Action = "POLICY_CANCELLED",
                ActionDate = DateTime.UtcNow,
                Remarks = "Policy cancelled via delete request"
            });

            await dbcontext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ActivatePolicyAsync(int id)
        {
            var policy = await dbcontext.Policies.FindAsync(id);

            if (policy == null)
            {
                return false;
            }

            policy.PolicyStatus = "Active";

            dbcontext.PolicyHistories.Add(new PolicyHistory
            {
                PolicyId = id,
                Action = "POLICY_ACTIVATED",
                ActionDate = DateTime.UtcNow,
                Remarks = "Policy activated after payment confirmation"
            });

            await dbcontext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SuspendPolicyAsync(int id)
        {
            var policy = await dbcontext.Policies.FindAsync(id);

            if (policy == null)
            {
                return false;
            }

            policy.PolicyStatus = "Suspended";

            dbcontext.PolicyHistories.Add(new PolicyHistory
            {
                PolicyId = id,
                Action = "POLICY_SUSPENDED",
                ActionDate = DateTime.UtcNow,
                Remarks = "Policy suspended"
            });

            await dbcontext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CancelPolicyAsync(int id)
        {
<<<<<<< HEAD
            var policy = await db.Policies.FindAsync(id);
=======
            var policy = await dbcontext.Policies.FindAsync(id);

>>>>>>> 8656f956a30e3ccf2e7ecb4ab01a6f97156463b2
            if (policy == null)
            {
                return false;
            }

            policy.PolicyStatus = "Cancelled";

            dbcontext.PolicyHistories.Add(new PolicyHistory
            {
                PolicyId = id,
                Action = "POLICY_CANCELLED",
                ActionDate = DateTime.UtcNow,
                Remarks = "Policy cancelled by admin"
            });

            await dbcontext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExpirePolicyAsync(int id)
        {
            var policy = await dbcontext.Policies.FindAsync(id);

            if (policy == null)
            {
                return false;
            }

            policy.PolicyStatus = "Expired";

            dbcontext.PolicyHistories.Add(new PolicyHistory
            {
                PolicyId = id,
                Action = "POLICY_EXPIRED",
                ActionDate = DateTime.UtcNow,
                Remarks = "Policy expired"
            });

            await dbcontext.SaveChangesAsync();
            return true;
        }
    }
}
