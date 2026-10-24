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
    public class PolicyEndorsementService : IPolicyEndorsementService
    {
        AppDbContext dbcontext;
        IMapper mapper;

        public PolicyEndorsementService(AppDbContext dbcontext, IMapper mapper)
        {
            this.dbcontext = dbcontext;
            this.mapper = mapper;
        }

        public async Task<PolicyEndorsementDto> CreateEndorsementAsync(int policyId, CreateEndorsementDto dto)
        {
            var policy = await dbcontext.Policies.FindAsync(policyId);
            if (policy == null)
            {
                throw new Exception("Policy not found");
            }
            var endorsement = mapper.Map<PolicyEndorsement>(dto);
            endorsement.PolicyId = policyId;
            endorsement.EndorsementDate = DateTime.UtcNow;

            dbcontext.PolicyEndorsements.Add(endorsement);

            dbcontext.PolicyHistories.Add(new PolicyHistory
            {
                PolicyId = policyId,
                Action = "ENDORSEMENT_REQUESTED",
                ActionDate = DateTime.UtcNow,
                Remarks = $"Endorsement requested: {dto.EndorsementType}. Old: '{dto.OldValue}', New: '{dto.NewValue}'"
            });
            await dbcontext.SaveChangesAsync();
            return mapper.Map<PolicyEndorsementDto>(endorsement);
        }

        public async Task<List<PolicyEndorsementDto>> GetEndorsementsByPolicyIdAsync(int policyId)
        {
            var endorsements = await dbcontext.PolicyEndorsements.Where(e => e.PolicyId == policyId).OrderByDescending(e => e.EndorsementDate).ToListAsync();
            return mapper.Map<List<PolicyEndorsementDto>>(endorsements);
        }

        public async Task<bool> ApproveEndorsementAsync(int endorsementId, int approvedBy)
        {
            var endorsement = await dbcontext.PolicyEndorsements.Include(e => e.Policy).FirstOrDefaultAsync(e => e.EndorsementId == endorsementId);
            if (endorsement == null)
            {
                return false;
            }
            endorsement.ApprovedBy = approvedBy;
            dbcontext.PolicyHistories.Add(new PolicyHistory
            {
                PolicyId = endorsement.PolicyId,
                Action = "ENDORSEMENT_APPROVED",
                ActionDate = DateTime.UtcNow,
                Remarks = $"Endorsement #{endorsementId} ({endorsement.EndorsementType}) approved by User ID {approvedBy}"
            });
            await dbcontext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RejectEndorsementAsync(int endorsementId)
        {
            var endorsement = await dbcontext.PolicyEndorsements.Include(e => e.Policy).FirstOrDefaultAsync(e => e.EndorsementId == endorsementId);
            if (endorsement == null)
            {
                return false;
            }
            endorsement.ApprovedBy = -1;
            dbcontext.PolicyHistories.Add(new PolicyHistory
            {
                PolicyId = endorsement.PolicyId,
                Action = "ENDORSEMENT_REJECTED",
                ActionDate = DateTime.UtcNow,
                Remarks = $"Endorsement #{endorsementId} ({endorsement.EndorsementType}) rejected"
            });
            await dbcontext.SaveChangesAsync();
            return true;
        }
    }
}
