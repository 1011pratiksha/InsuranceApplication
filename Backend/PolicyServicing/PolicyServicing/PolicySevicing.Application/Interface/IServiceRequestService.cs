using PolicySevicing.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolicySevicing.Application.Interface
{
    public interface IServiceRequestService
    {
        Task CreateServiceRequest(CreateServiceRequestDto dto);
        Task<List<ServiceRequestResponseDto>> GetAllServiceRequests();
        Task<ServiceRequestResponseDto?> GetServiceRequestById(long id);
        Task UpdateServiceRequest(long id,UpdateServiceRequestDto dto);
    }
}
