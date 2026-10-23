using PolicySevicing.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolicySevicing.Application.Interface
{
    public interface ICancellationService
    {
        Task<string> CancelPolicy(long policyId, CancelPolicyDto dto);
        Task<CancellationDto?> CancellationById(long id);
    }
}
