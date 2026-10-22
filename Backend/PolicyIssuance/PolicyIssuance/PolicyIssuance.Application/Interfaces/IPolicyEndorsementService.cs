using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PolicyIssuance.Application.DTO;

namespace PolicyIssuance.Application.Interfaces
{
    public interface IPolicyEndorsementService
    {
        Task<PolicyEndorsementDto> CreateEndorsementAsync(int policyId, CreateEndorsementDto dto);

        Task<List<PolicyEndorsementDto>> GetEndorsementsByPolicyIdAsync(int policyId);

        Task<bool> ApproveEndorsementAsync(int endorsementId, int approvedBy);

        Task<bool> RejectEndorsementAsync(int endorsementId);
    }
}
