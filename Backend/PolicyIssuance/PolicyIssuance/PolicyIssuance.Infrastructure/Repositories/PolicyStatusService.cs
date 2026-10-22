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
    public class PolicyStatusService : IPolicyStatusService
    {
        AppDbContext dbcontext;
        IMapper mapper;

        public PolicyStatusService(AppDbContext dbcontext, IMapper mapper)
        {
            this.dbcontext = dbcontext;
            this.mapper = mapper;
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
