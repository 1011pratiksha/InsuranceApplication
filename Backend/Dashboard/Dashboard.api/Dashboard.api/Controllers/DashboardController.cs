using Dashboard.application.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Dashboard.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardRepository repository;

        public DashboardController(IDashboardRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet("Admin")]
        public async Task<IActionResult> GetAdminDashboard()
        {
            var result = await repository.GetAdminDashboardAsync();
            return Ok(result);
        }

        [HttpGet("Agent/{agentId}")]
        public async Task<IActionResult> GetAgentDashboard(int agentId)
        {
            var result = await repository.GetAgentDashboardAsync(agentId);
            return Ok(result);
        }

        [HttpGet("Customer/{customerId}")]
        public async Task<IActionResult> GetCustomerDashboard(int customerId)
        {
            var result = await repository.GetCustomerDashboardAsync(customerId);
            return Ok(result);
        }
    }
}
