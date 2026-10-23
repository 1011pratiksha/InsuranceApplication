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

        public PolicyService(AppDbContext dbcontext, IMapper mapper)
        {
            this.dbcontext = dbcontext;
            this.mapper = mapper;
        }

        //public async Task<PolicyDetailDto> IssuePolicyAsync(IssuePolicyDto dto)
        //{
        //    var quote = await dbcontext.Quotes.FindAsync(dto.QuoteId);
        //    if (quote == null)
        //    {
        //        throw new Exception("Quote not found");
        //    }

        //    var customer = await dbcontext.Customers.FindAsync(dto.CustomerId);
        //    if (customer == null)
        //    {
        //        throw new Exception("Customer not found");
        //    }

        //    var product = await dbcontext.InsuranceProducts.FindAsync(dto.ProductId);
        //    if (product == null)
        //    {
        //        throw new Exception("Product not found");
        //    }

        //    var policyNumber = $"POL-{DateTime.UtcNow:yyyy}-{Guid.NewGuid().ToString("N")[..8].ToUpper()}";

        //    var policy = new Policy
        //    {
        //        PolicyNumber = policyNumber,
        //        QuoteId = dto.QuoteId,
        //        CustomerId = dto.CustomerId,
        //        ProductId = dto.ProductId,
        //        IssueDate = DateTime.UtcNow,
        //        EffectiveDate = dto.EffectiveDate,
        //        ExpiryDate = dto.ExpiryDate,
        //        PolicyStatus = "Draft"
        //    };

        //    if (dto.Coverages != null && dto.Coverages.Count > 0)
        //    {
        //        policy.PolicyCoverages = mapper.Map<List<PolicyCoverage>>(dto.Coverages);
        //    }

        //    if (dto.Riders != null && dto.Riders.Count > 0)
        //    {
        //        policy.PolicyRiders = mapper.Map<List<PolicyRider>>(dto.Riders);
        //    }

        //    policy.PolicyHistories.Add(new PolicyHistory
        //    {
        //        Action = "POLICY_CREATED",
        //        ActionDate = DateTime.UtcNow,
        //        Remarks = "Policy created in draft status"
        //    });

        //    dbcontext.Policies.Add(policy);
        //    await dbcontext.SaveChangesAsync();
        //    return mapper.Map<PolicyDetailDto>(policy);
        //}

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
