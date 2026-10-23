using PolicySevicing.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolicySevicing.Application.Interface
{
    public interface IRenewalService
    {
        Task<string> RenewPolicy(long pid, RenewPolicyDto dto);

        Task<RenewalDto2> RenewalbyId(long id);
    }
}
