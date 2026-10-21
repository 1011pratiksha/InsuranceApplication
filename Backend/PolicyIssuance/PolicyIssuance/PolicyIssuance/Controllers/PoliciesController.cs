using Microsoft.AspNetCore.Mvc;
using PolicyIssuance.Application.DTO;
using PolicyIssuance.Application.Interfaces;
using PolicyIssuance.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PolicyIssuance.Controllers
{
    [ApiController]
    [Route("api/v1/policies")]
    public class PoliciesController : ControllerBase
    {
        private readonly IPolicyService db;

        public PoliciesController(IPolicyService db)
        {
            this.db = db;
        }

        [HttpPost]
        public async Task<IActionResult> IssuePolicy(IssuePolicyDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await db.IssuePolicyAsync(dto);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPolicies()
        {
            var result = await db.GetAllPoliciesAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPolicyById(int id)
        {
            var result = await db.GetPolicyByIdAsync(id);

            if (result == null)
            {
                return NotFound("Policy not found");
            }

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePolicy(int id, UpdatePolicyDto dto)
        {
            var result = await db.UpdatePolicyAsync(id, dto);

            if (result == null)
            {
                return NotFound("Policy not found");
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePolicy(int id)
        {
            var result = await db.DeletePolicyAsync(id);

            if (!result)
            {
                return NotFound("Policy not found");
            }

            return Ok("Policy cancelled successfully");
        }

        [HttpPut("{id}/activate")]
        public async Task<IActionResult> ActivatePolicy(int id)
        {
            var result = await db.ActivatePolicyAsync(id);

            if (!result)
            {
                return NotFound("Policy not found");
            }

            return Ok("Policy activated successfully");
        }

        [HttpPut("{id}/suspend")]
        public async Task<IActionResult> SuspendPolicy(int id)
        {
            var result = await db.SuspendPolicyAsync(id);

            if (!result)
            {
                return NotFound("Policy not found");
            }

            return Ok("Policy suspended successfully");
        }

        [HttpPut("{id}/cancel")]
        public async Task<IActionResult> CancelPolicy(int id)
        {
            var result = await db.CancelPolicyAsync(id);

            if (!result)
            {
                return NotFound("Policy not found");
            }

            return Ok("Policy cancelled successfully");
        }

        [HttpPut("{id}/expire")]
        public async Task<IActionResult> ExpirePolicy(int id)
        {
            var result = await db.ExpirePolicyAsync(id);

            if (!result)
            {
                return NotFound("Policy not found");
            }

            return Ok("Policy expired successfully");
        }
    }
}
