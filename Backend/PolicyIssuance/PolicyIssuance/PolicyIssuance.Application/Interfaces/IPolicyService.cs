using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PolicyIssuance.Application.DTO;

namespace PolicyIssuance.Application.Interfaces
{
    public interface IPolicyService
    {
     //   Task<PolicyDetailDto> IssuePolicyAsync(IssuePolicyDto dto);

        Task<List<PolicyDto>> GetAllPoliciesAsync();

        Task<PolicyDetailDto?> GetPolicyByIdAsync(int id);

        Task<PolicyDetailDto?> UpdatePolicyAsync(int id, UpdatePolicyDto dto);

        Task<bool> DeletePolicyAsync(int id);

        Task<bool> ActivatePolicyAsync(int id);

        Task<bool> SuspendPolicyAsync(int id);

        Task<bool> CancelPolicyAsync(int id);

        Task<bool> ExpirePolicyAsync(int id);
    }
}
