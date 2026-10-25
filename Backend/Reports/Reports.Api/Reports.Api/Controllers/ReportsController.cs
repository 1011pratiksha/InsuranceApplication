using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reports.Application.DTO;
using Reports.Application.Interface;
using Reports.Api.Wrappers;
using Microsoft.AspNetCore.Authorization;


namespace Reports.Api.Controllers
{
    [Route("api/v1/reports")]
    [ApiController]
    [Authorize]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _service;

        public ReportsController(IReportService service)
        {
            _service = service;
        }

        [HttpGet("customers")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetCustomers(
            [FromQuery] ReportFilterDto filter)
        {
            var data = await _service.GetCustomersAsync(filter);
            return Ok(new ApiResponse<object>(true,
                "Customer report retrieved successfully", data));
        }

        [HttpGet("policies")]
        public async Task<IActionResult> GetPolicies(
            [FromQuery] ReportFilterDto filter)
        {
            var data = await _service.GetPoliciesAsync(filter);
            return Ok(new ApiResponse<object>(true,
                "Policy report retrieved successfully", data));
        }

        [HttpGet("premiums")]
        public async Task<IActionResult> GetPremiums(
            [FromQuery] ReportFilterDto filter)
        {
            var data = await _service.GetPremiumsAsync(filter);
            return Ok(new ApiResponse<object>(true,
                "Premium report retrieved successfully", data));
        }

        [HttpGet("claims")]
        public async Task<IActionResult> GetClaims(
            [FromQuery] ReportFilterDto filter)
        {
            var data = await _service.GetClaimsAsync(filter);
            return Ok(new ApiResponse<object>(true,
                "Claims report retrieved successfully", data));
        }

        [HttpGet("underwriting")]
        public async Task<IActionResult> GetUnderwriting(
            [FromQuery] ReportFilterDto filter)
        {
            var data = await _service.GetUnderwritingAsync(filter);
            return Ok(new ApiResponse<object>(true,
                "Underwriting report retrieved successfully", data));
        }

        [HttpGet("agents")]
        public async Task<IActionResult> GetAgents(
            [FromQuery] ReportFilterDto filter)
        {
            var data = await _service.GetAgentsAsync(filter);
            return Ok(new ApiResponse<object>(true,
                "Agent report retrieved successfully", data));
        }

        [HttpGet("financial")]
        public async Task<IActionResult> GetFinancial(
            [FromQuery] ReportFilterDto filter)
        {
            var data = await _service.GetFinancialAsync(filter);
            return Ok(new ApiResponse<object>(true,
                "Financial report retrieved successfully", data));
        }
    }
}
