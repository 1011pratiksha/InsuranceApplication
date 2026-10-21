using PolicySevicing.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolicySevicing.Application.Interface
{
    public interface IEndorsementService
    {
        Task createEndorsement(EndorsementDto dto);
        Task<EndorsementDto2> fetchendorse(long id);

        Task ApprovedEnd(long id);
        Task RejectEnd(long id);
    }
}
