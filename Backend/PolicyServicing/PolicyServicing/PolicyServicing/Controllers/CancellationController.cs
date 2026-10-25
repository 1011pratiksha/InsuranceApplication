using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PolicySevicing.Application.ApiResponse;
using PolicySevicing.Application.DTO;
using PolicySevicing.Application.Interface;

namespace PolicyServicing.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class CancellationController : ControllerBase
    {
        ICancellationService service;
        public CancellationController(ICancellationService service)
        {
            this.service = service;
        }

        [HttpPost("policies/{id}")]
        public async Task<ApisResponse<string>> CancelPolicy(long id , CancelPolicyDto dto)
        {
           var res= await service.CancelPolicy(id, dto);
            return ApisResponse<string>.SuccessResponse(null, res);
        }

        [HttpGet("cancellations/{id:long}")]
        public async Task<ApisResponse<CancellationDto>> CancellationById(long id)
        {
            var result = await service.CancellationById(id);
            return ApisResponse<CancellationDto>.SuccessResponse(result);
        }

    }
}
