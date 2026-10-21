using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PolicySevicing.Application.ApiResponse;
using PolicySevicing.Application.DTO;
using PolicySevicing.Application.Interface;

namespace PolicyServicing.Controllers
{
   
    [ApiController]
    public class RenewalController : ControllerBase
    {
        IRenewalService service;
        public RenewalController(IRenewalService service)
        {
            this.service = service;
        }

        [HttpPost("api/v1/policies/{id}/renew")]
        public async Task<ApisResponse<string>> RenewPolicy(long id,RenewPolicyDto dto)
        {
            var result= await service.RenewPolicy(id, dto);
             return ApisResponse<string>.SuccessResponse(null, result);
        }

        [HttpGet("api/v1/renewals/{id}")]
        public async Task<ApisResponse<RenewalDto2>> RenewalbyId(long id)
        {
            var res = await service.RenewalbyId(id);
            return ApisResponse<RenewalDto2>.SuccessResponse(res);
        }
    }
}
