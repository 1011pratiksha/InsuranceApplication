using Microsoft.AspNetCore.Mvc;
using PremiumPayment.Application.DTO;
using PremiumPayment.Application.Interfaces;
using PremiumPayment.Application.Common;

namespace PremiumPayment.API.Controllers
{
    [Route("api/v1/premium-schedules")]
    [ApiController]
    public class PremiumScheduleController : ControllerBase
    {
        IPremiumScheduleService service;
        public PremiumScheduleController(IPremiumScheduleService service)
        {
            this.service = service;
        }

        [HttpPost]
        public async Task<IActionResult> GeneratePremiumSchedule(PremiumScheduleRequestDto request)
        {
            try
            {
                var data = await service.GeneratePremiumSchedule(request);
                var response = new ApiResponse<List<PremiumScheduleResponseDto>>
                {
                    Success = true,
                    Message = "Premium Schedule Generated Successfully",
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
                    Message = "Premium Schedule Generation Failed",
                    Data = null,
                    Error = ex.Message
                };
                return BadRequest(response);
            }
        }

        [HttpGet]
        [Route("{policyId}")]
        public async Task<IActionResult> GetPremiumSchedule(int policyId)
        {
            var data = await service.GetPremiumSchedule(policyId);

            if (data.Count == 0)
            {
                var notFound = new ApiResponse<object>
                {
                    Success = false,
                    Message = "Premium Schedule Not Found",
                    Data = null,
                    Error = "No premium schedule exists for the given policy ID"
                };
                return NotFound(notFound);
            }

            var response = new ApiResponse<List<PremiumScheduleResponseDto>>
            {
                Success = true,
                Message = "Premium Schedule Fetched Successfully",
                Data = data,
                Error = null
            };
            return Ok(response);
        }

        [HttpPut]
        [Route("{scheduleId}")]
        public async Task<IActionResult> UpdateInstallment(int scheduleId, UpdateInstallmentDto request)
        {
            try
            {
                var data = await service.UpdateInstallment(scheduleId, request);

                if (data == null)
                {
                    var notFound = new ApiResponse<object>
                    {
                        Success = false,
                        Message = "Installment Not Found",
                        Data = null,
                        Error = "No installment exists with the given schedule ID"
                    };
                    return NotFound(notFound);
                }

                var response = new ApiResponse<PremiumScheduleResponseDto>
                {
                    Success = true,
                    Message = "Installment Updated Successfully",
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
                    Message = "Installment Update Failed",
                    Data = null,
                    Error = ex.Message
                };
                return BadRequest(response);
            }
        }
    }
}