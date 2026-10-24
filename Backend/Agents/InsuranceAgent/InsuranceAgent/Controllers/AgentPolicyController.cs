using InsuranceAgent.Application.DTO;
using InsuranceAgent.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceAgent.Controllers
{
    [Route("api/v1/agents/{agentId}/policies")]
    [ApiController]
    public class AgentPolicyController : ControllerBase
    {
        IAgentPolicyService service;


        public AgentPolicyController(IAgentPolicyService service)
        {
            this.service= service;
        }


        [HttpPost]
        public async Task<IActionResult> CreateAgentPolicy(int agentId, CreateAgentPolicyDto dto)
        {
            try
            {
                await service.CreateAgentPolicy(agentId, dto);

                return Ok("Agent policy assigned successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetAgentPolicies(int agentId)
        {
            try
            {
                var policies = await service.GetAgentPolicies(agentId);

                return Ok(policies);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("{policyId}")]
        public async Task<IActionResult> GetAgentPolicyById(int agentId, int policyId)
        {
            try
            {
                var policy = await service.GetAgentPolicyById(agentId, policyId);

                return Ok(policy);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("/api/v1/agents/customers")]
        public async Task<IActionResult> GetAllCustomers()
        {
            try
            {
                var customers = await service.GetAllCustomers();
                return Ok(customers);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("/api/v1/agents/all-policies")]
        public async Task<IActionResult> GetAllPolicies()
        {
            try
            {
                var policies = await service.GetAllPolicies();
                return Ok(policies);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
