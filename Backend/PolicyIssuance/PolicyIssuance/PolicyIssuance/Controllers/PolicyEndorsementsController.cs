using Microsoft.AspNetCore.Mvc;
using PolicyIssuance.Application.DTO;
using PolicyIssuance.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PolicyIssuance.Controllers
{
    [ApiController]
    public class PolicyEndorsementsController : ControllerBase
    {
        private readonly IPolicyEndorsementService _endorsementService;

        public PolicyEndorsementsController(IPolicyEndorsementService endorsementService)
        {
            _endorsementService = endorsementService;
        }

        [HttpPost("api/v1/policies/{policyId}/endorsements")]
        public async Task<IActionResult> CreateEndorsement(int policyId, [FromBody] CreateEndorsementDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _endorsementService.CreateEndorsementAsync(policyId, dto);
                return Created($"/api/v1/endorsements/{result.EndorsementId}", result);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Policy not found"))
                {
                    return NotFound(ex.Message);
                }

                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("api/v1/policies/{policyId}/endorsements")]
        public async Task<IActionResult> GetEndorsementsByPolicyId(int policyId)
        {
            var result = await _endorsementService.GetEndorsementsByPolicyIdAsync(policyId);
            return Ok(result);
        }

        [HttpPut("api/v1/endorsements/{endorsementId}/approve")]
        public async Task<IActionResult> ApproveEndorsement(int endorsementId, [FromQuery] int approvedBy = 1)
        {
            var success = await _endorsementService.ApproveEndorsementAsync(endorsementId, approvedBy);
            if (!success)
            {
                return NotFound("Endorsement not found");
            }

            return Ok("Endorsement approved successfully");
        }

        [HttpPut("api/v1/endorsements/{endorsementId}/reject")]
        public async Task<IActionResult> RejectEndorsement(int endorsementId)
        {
            var success = await _endorsementService.RejectEndorsementAsync(endorsementId);
            if (!success)
            {
                return NotFound("Endorsement not found");
            }

            return Ok("Endorsement rejected successfully");
        }
    }
}
