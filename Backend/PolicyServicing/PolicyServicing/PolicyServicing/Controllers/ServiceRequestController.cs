using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PolicySevicing.Application.ApiResponse;
using PolicySevicing.Application.DTO;
using PolicySevicing.Application.Interface;

namespace PolicyServicing.Controllers
{
    [Route("api/v1/service-requests")]
    [ApiController]
    public class ServiceRequestController : ControllerBase
    {
        IServiceRequestService service;
        public ServiceRequestController(IServiceRequestService service)
        {
            this.service = service;
        }
        [HttpPost]
        [Route("create-requests")]
        public async Task<ApisResponse<IActionResult>> CreateServiceRequest(CreateServiceRequestDto dto)
        {
            await service.CreateServiceRequest(dto);
            return ApisResponse<IActionResult>.SuccessResponse(null, "Request Created Sucessfully");
        }

        [HttpGet]
        [Route("get-requests")]
        public async Task<ApisResponse<List<ServiceRequestResponseDto>>> GetAllRequests()
        {
            var reqs = await service.GetAllServiceRequests();
            return ApisResponse<List<ServiceRequestResponseDto>>.SuccessResponse(reqs, "Requests fetched successfully");

        }

        [HttpGet]
        [Route("get-request/{id}")]
        public async Task<ApisResponse<ServiceRequestResponseDto>> GetRequestById(long id)
        {
            var res = await service.GetServiceRequestById(id);
            if (res == null)
            {
                return ApisResponse<ServiceRequestResponseDto>.ErrorResponse(null, "Not Found");
            }
            return ApisResponse<ServiceRequestResponseDto>.SuccessResponse(res, "Request Success");
        }

        [HttpPut("update-request/{id}")]
        public async Task<ApisResponse<IActionResult>> UpdateReq(long id, UpdateServiceRequestDto dto)
        {
            var res =  service.UpdateServiceRequest(id, dto);
            return ApisResponse<IActionResult>.SuccessResponse(null, "Updated Successfully");

        }

        [HttpPut("update-status/{id}")]
        public async Task<ApisResponse<IActionResult>> UpdateStatus( long id,UpdateServiceRequestDto dto)
        {
            var updated = await service.UpdateServiceRequestStatus(id, dto);
            if (!updated)
            {
                return ApisResponse<IActionResult>.ErrorResponse(
                    null, "Service request not found");
            }
            return ApisResponse<IActionResult>.SuccessResponse(null, "Request status updated successfully");
        }
    }
}
