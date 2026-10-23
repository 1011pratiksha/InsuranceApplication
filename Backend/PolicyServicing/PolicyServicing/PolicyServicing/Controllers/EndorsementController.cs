using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PolicySevicing.Application.ApiResponse;
using PolicySevicing.Application.DTO;
using PolicySevicing.Application.Interface;

namespace PolicyServicing.Controllers
{
    [Route("api/v1/endorsements")]
    [ApiController]
    public class EndorsementController : ControllerBase
    {
        IEndorsementService service;
        public EndorsementController(IEndorsementService service)
        {
            this.service = service;
        }

        [HttpPost]
        public async Task<ApisResponse<IActionResult>> AddEnd(EndorsementDto dto)
        {
             await service.createEndorsement(dto);
            return ApisResponse<IActionResult>.SuccessResponse(null, "Endorsement created successfully");
        }

        [HttpGet("{id}")]
        public async Task<ApisResponse<EndorsementDto2>> GetEnd(long id)
        {
            var res = await service.fetchendorse(id);
            return ApisResponse<EndorsementDto2>.SuccessResponse(res, "Success");
        }

        [HttpPut("approve/{id}")]
        public async Task<ApisResponse<IActionResult>> ApprovEnd(long id)
        {
            await service.ApprovedEnd(id);
            return ApisResponse<IActionResult>.SuccessResponse(null, "Endorsement approved successfully");
        }

        [HttpPut("reject/{id}")]
        public async Task<ApisResponse<IActionResult>> RejecrEnd(long id)
        {
            await service.RejectEnd(id);
            return ApisResponse<IActionResult>.SuccessResponse(null, "Endorsement Rejected successfully");
        }
    }
}
