using PolicyRenewal.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolicyRenewal.Application.Interface
{
    public interface IPolicyRenewalService
    {
        Task<PolicyRenewalResponseDto> RenewPolicy(int policyId, RenewPolicyRequestDto request);
        Task<PolicyRenewalResponseDto> GetRenewal(int renewalId);

        Task ExpirePendingRenewals();
        Task SendRenewalReminder();
    }
}
