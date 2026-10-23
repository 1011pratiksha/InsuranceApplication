using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PolicyIssuance.Application.DTO;

namespace PolicyIssuance.Application.Interfaces
{
    public interface IPolicyStatusService
    {
        
        Task<bool> ActivatePolicyAsync(int id);

        Task<bool> SuspendPolicyAsync(int id);

        Task<bool> CancelPolicyAsync(int id);

        Task<bool> ExpirePolicyAsync(int id);
    }
}
