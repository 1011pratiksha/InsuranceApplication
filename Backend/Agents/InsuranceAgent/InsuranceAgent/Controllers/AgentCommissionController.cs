using InsuranceAgent.Application.DTO;
using InsuranceAgent.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceAgent.Controllers
{
    [Route("api/v1/commissions")]
    [ApiController]
    public class AgentCommissionController : ControllerBase
    {
        IAgentCommissionService service;

        public AgentCommissionController(IAgentCommissionService service)
        {
            this.service = service;
        }


        [HttpPost]
        [Route("calculate")]
        public async Task<IActionResult> CalculateCommission(CalculateCommissionDto dto)
        {
            try
            {
                await service.CalculateCommission(dto);

                return Ok("Commission calculated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        [HttpGet]
        public async Task<IActionResult> GetAllCommissions(string status, int? agentId, DateTime? fromDate, DateTime? toDate)
        {
            try
            {
                var commissions = await service.GetAllCommissions(
                    status, agentId, fromDate, toDate);

                return Ok(commissions);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        [HttpGet]
        [Route("/api/v1/agents/{agentId}/commissions")]
        public async Task<IActionResult> GetCommissionsByAgentId(int agentId)
        {
            try
            {
                var commissions = await service.GetCommissionsByAgentId(agentId);

                return Ok(commissions);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPatch]
        [Route("{commissionId}/status")]
        public async Task<IActionResult> UpdateCommissionStatus(int commissionId, CommissionStatusUpdateDto dto)
        {
            try
            {
                await service.UpdateCommissionStatus(commissionId, dto);

                return Ok("Commission status updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
