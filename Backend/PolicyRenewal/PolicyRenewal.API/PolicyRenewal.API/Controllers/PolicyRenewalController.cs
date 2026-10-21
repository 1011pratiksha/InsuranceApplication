using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PolicyRenewal.Application.DTO;
using PolicyRenewal.Application.Interface;
using PolicyRenewal.Domain.Entities;

namespace PolicyRenewal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolicyRenewalController : ControllerBase
    {
        IPolicyRenewalService service;
        public PolicyRenewalController(IPolicyRenewalService service)
        {
            this.service = service;
        }

        [HttpPost("policies/{policyId}/renew")]
        public async Task<IActionResult> RenewPolicy(int policyId, RenewPolicyRequestDto request)
        {
            try
            {
                var data = await service.RenewPolicy(policyId, request);

                var response = new ApiResponse<PolicyRenewalResponseDto>
                {
                    Success = true,
                    Message = data.RenewalStatus == "Renewed"
                        ? "Policy Renewed Successfully"
                        : "Renewal Confirmed, Waiting For Premium Payment",
                    Data = data,
                    Error = null
                };
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                var response = new ApiResponse<object>
                {
                    Success = false,
                    Message = "Policy Renewal Failed",
                    Data = null,
                    Error = ex.Message
                };
                return BadRequest(response);
            }
        }

        [HttpGet("renewals/{renewalId}")]
        public async Task<IActionResult> GetRenewal(int renewalId)
        {
            var data = await service.GetRenewal(renewalId);

            if (data == null)
            {
                var notFound = new ApiResponse<object>
                {
                    Success = false,
                    Message = "Renewal Not Found",
                    Data = null,
                    Error = "No renewal exists with the given renewal ID"
                };
                return NotFound(notFound);
            }

            var response = new ApiResponse<PolicyRenewalResponseDto>
            {
                Success = true,
                Message = "Renewal Fetched Successfully",
                Data = data,
                Error = null
            };
            return Ok(response);
        }
    }
}
