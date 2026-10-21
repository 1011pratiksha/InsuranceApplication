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
        AppDbContext db;
        IMapper mapper;

        public PolicyEndorsementService(AppDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<PolicyEndorsementDto> CreateEndorsementAsync(int policyId, CreateEndorsementDto dto)
        {
            var policy = await db.Policies.FindAsync(policyId);
            if (policy == null)
            {
                throw new Exception("Policy not found");
            }

            var endorsement = mapper.Map<PolicyEndorsement>(dto);
            endorsement.PolicyId = policyId;
            endorsement.EndorsementDate = DateTime.UtcNow;

            db.PolicyEndorsements.Add(endorsement);

            db.PolicyHistories.Add(new PolicyHistory
            {
                PolicyId = policyId,
                Action = "ENDORSEMENT_REQUESTED",
                ActionDate = DateTime.UtcNow,
                Remarks = $"Endorsement requested: {dto.EndorsementType}. Old: '{dto.OldValue}', New: '{dto.NewValue}'"
            });

            await db.SaveChangesAsync();
            return mapper.Map<PolicyEndorsementDto>(endorsement);
        }

        public async Task<List<PolicyEndorsementDto>> GetEndorsementsByPolicyIdAsync(int policyId)
        {
            var endorsements = await db.PolicyEndorsements
                .Where(e => e.PolicyId == policyId)
                .OrderByDescending(e => e.EndorsementDate)
                .ToListAsync();

            return mapper.Map<List<PolicyEndorsementDto>>(endorsements);
        }

        public async Task<bool> ApproveEndorsementAsync(int endorsementId, int approvedBy)
        {
            var endorsement = await db.PolicyEndorsements
                .Include(e => e.Policy)
                .FirstOrDefaultAsync(e => e.EndorsementId == endorsementId);

            if (endorsement == null)
            {
                return false;
            }

            endorsement.ApprovedBy = approvedBy;

            db.PolicyHistories.Add(new PolicyHistory
            {
                PolicyId = endorsement.PolicyId,
                Action = "ENDORSEMENT_APPROVED",
                ActionDate = DateTime.UtcNow,
                Remarks = $"Endorsement #{endorsementId} ({endorsement.EndorsementType}) approved by User ID {approvedBy}"
            });

            await db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RejectEndorsementAsync(int endorsementId)
        {
            var endorsement = await db.PolicyEndorsements
                .Include(e => e.Policy)
                .FirstOrDefaultAsync(e => e.EndorsementId == endorsementId);

            if (endorsement == null)
            {
                return false;
            }

            endorsement.ApprovedBy = -1;

            db.PolicyHistories.Add(new PolicyHistory
            {
                PolicyId = endorsement.PolicyId,
                Action = "ENDORSEMENT_REJECTED",
                ActionDate = DateTime.UtcNow,
                Remarks = $"Endorsement #{endorsementId} ({endorsement.EndorsementType}) rejected"
            });

            await db.SaveChangesAsync();
            return true;
        }
    }
}
