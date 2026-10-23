using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reports.Application.DTO;
using Reports.Application.Interface;
using Reports.Api.Wrappers;

namespace Reports.Api.Controllers
{
    [Route("api/v1/reports/schedule")]
    [ApiController]
    public class ReportScheduleController : ControllerBase
    {
        private readonly IReportScheduleService _service;

        public ReportScheduleController(IReportScheduleService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateReportScheduleDto dto)
        {
            var data = await _service.CreateAsync(dto);

            return Ok(new ApiResponse<object>(true,
                "Report schedule created successfully", data));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAllAsync();

            return Ok(new ApiResponse<object>(true,
                "Report schedules retrieved successfully", data));
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Update(
            long id,
            [FromBody] UpdateReportScheduleDto dto)
        {
            var data = await _service.UpdateAsync(id, dto);

            if (data == null)
            {
                return NotFound(new ApiResponse<object>(false,
                    "Report schedule not found", null));
            }

            return Ok(new ApiResponse<object>(true,
                "Report schedule updated successfully", data));
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            var deleted = await _service.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound(new ApiResponse<object>(false,
                    "Report schedule not found", null));
            }

            return Ok(new ApiResponse<object>(true,
                "Report schedule deleted successfully", null));
        }
    }
}
