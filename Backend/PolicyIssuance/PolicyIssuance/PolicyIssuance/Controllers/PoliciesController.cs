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
        private readonly IPolicyService service;

        public PoliciesController(IPolicyService service)
        {
            this.service = service;
        }

        [HttpPost]
        public async Task<IActionResult> IssuePolicy(IssuePolicyDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await service.IssuePolicyAsync(dto);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPolicies()
        {
            var result = await service.GetAllPoliciesAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPolicyById(int id)
        {
            var result = await service.GetPolicyByIdAsync(id);
            if (result == null)
            {
                return NotFound("Policy not found");
            }
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePolicy(int id, UpdatePolicyDto dto)
        {
            var result = await service.UpdatePolicyAsync(id, dto);
            if (result == null)
            {
                return NotFound("Policy not found");
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePolicy(int id)
        {
            var result = await service.DeletePolicyAsync(id);
            if (!result)
            {
                return NotFound("Policy not found");
            }
            return Ok("Policy cancelled successfully");
        }     
    }
}
