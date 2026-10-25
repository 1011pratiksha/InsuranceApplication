using Dashboard.application.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Dashboard.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardRepository repository;

        public DashboardController(IDashboardRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet("Admin")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAdminDashboard()
        {
            var result = await repository.GetAdminDashboardAsync();
            return Ok(result);
        }

        [HttpGet("Agent/{agentId}")]
        [Authorize(Roles = "Agent")]
        public async Task<IActionResult> GetAgentDashboard(int agentId)
        {
            var result = await repository.GetAgentDashboardAsync(agentId);
            return Ok(result);
        }

        [HttpGet("Customer/{customerId}")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetCustomerDashboard(int customerId)
        {
            var result = await repository.GetCustomerDashboardAsync(customerId);
            return Ok(result);
        }
    }
}



