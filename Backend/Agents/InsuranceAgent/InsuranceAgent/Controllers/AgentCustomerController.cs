
using InsuranceAgent.Application.DTO;
using InsuranceAgent.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceAgent.Controllers
{
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
                await service.AssignCustomer(agentId, dto);

                return Ok(new { msg = "Customer assigned successfully." });
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

        [HttpPut]
        [Route("{agentId}/customers/{customerId}/transfer")]
        public async Task<IActionResult> TransferCustomer(int agentId, int customerId, TransferAgentCustomerDto dto)
        {
            try
            {
                await service.TransferCustomer(agentId, customerId, dto);

                return Ok("Customer transferred successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = ex.Message });
            }
        }
    }
}
