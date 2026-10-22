
using InsuranceAgent.Application.DTO;
using InsuranceAgent.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceAgent.Controllers
{
    [Route("api/v1/agents")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        IAgentService service;

        public AgentsController(IAgentService service)
        {
            this.service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAgent(CreateAgentDto dto)
        {
            try
            {
                await service.CreateAgent(dto);

                return Ok(new { msg = "Agent created successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAgents()
        {
            var agents = await service.GetAllAgents();

            if (agents == null)
            {
                return NotFound(new { msg = "No agents found." });
            }

            return Ok(agents);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetAgentById(int id)
        {
            var agent = await service.GetAgentById(id);

            if (agent == null)
            {
                return NotFound(new { msg = "Agent not found." });
            }

            return Ok(agent);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateAgent(int id, UpdateAgentDto dto)
        {
            try
            {
                await service.UpdateAgent(id, dto);

                return Ok(new { msg = "Agent updated successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = ex.Message });
            }
        }
    }
}
