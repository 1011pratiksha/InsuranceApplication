
using InsuranceAgent.Application.DTO;
using InsuranceAgent.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace InsuranceAgent.Controllers
{
    [Authorize]
    [Route("api/v1/agents")]
    [ApiController]
    public class AgentCustomerController : ControllerBase
    {
        IAgentCustomerService service;

        public AgentCustomerController(IAgentCustomerService service)
        {
            this.service = service;
        }


        [HttpPost]
        [Route("{agentId}/customers")]
        public async Task<IActionResult> AssignCustomer(int agentId, CreateAgentCustomerDto dto)
        {
            try
            {
                var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (!int.TryParse(userIdValue, out int userId))
                {
                    return Unauthorized(new { msg = "Unable to identify logged-in user." });
                }

                await service.AssignCustomer(agentId, dto, userId);

                return Ok(new{ msg = "Customer assigned successfully."});
                }
                catch (Exception ex)
                {
                    return BadRequest(new { msg = ex.Message });
                }
        }



        [HttpGet]
        [Route("{agentId}/customers")]
        public async Task<IActionResult> GetAssignedCustomers(int agentId)
        {
            var customers = await service.GetAssignedCustomers(agentId);

            if (customers == null)
            {
                return NotFound(new { msg = "No customers found." });
            }

            return Ok(customers);
        }


        [HttpGet]
        [Route("available-customers")]
        public async Task<IActionResult> GetAllCustomers()
        {
            try
            {
                var customers = await service.GetAllCustomers();
                return Ok(customers);
            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = ex.Message });
            }
        }



        [HttpPut]
        [Route("{agentId}/customers/{customerId}/transfer")]
        public async Task<IActionResult> TransferCustomer(int agentId, int customerId, TransferAgentCustomerDto dto)
        {
            try
            {
                var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (!int.TryParse(userIdValue, out int userId))
                {
                    return Unauthorized(new { msg = "Unable to identify logged-in user." });
                }

                await service.TransferCustomer(agentId, customerId, dto, userId);

                return Ok(new { msg = "Customer transferred successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = ex.Message });
            }
        }
    }
}
