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
    public class PolicyStatusController : ControllerBase
    {
        private readonly IPolicyService statusservice;

        public PolicyStatusController(IPolicyService statusservice)
        {
            this.statusservice = statusservice;
        }        

        [HttpPut("{id}/activate")]
        public async Task<IActionResult> ActivatePolicy(int id)
        {
            var result = await statusservice.ActivatePolicyAsync(id);
            if (!result)
            {
                return NotFound("Policy not found");
            }
            return Ok("Policy activated successfully");
        }

        [HttpPut("{id}/suspend")]
        public async Task<IActionResult> SuspendPolicy(int id)
        {
            var result = await statusservice.SuspendPolicyAsync(id);
            if (!result)
            {
                return NotFound("Policy not found");
            }
            return Ok("Policy suspended successfully");
        }

        [HttpPut("{id}/cancel")]
        public async Task<IActionResult> CancelPolicy(int id)
        {
            var result = await statusservice.CancelPolicyAsync(id);
            if (!result)
            {
                return NotFound("Policy not found");
            }
            return Ok("Policy cancelled successfully");
        }

        [HttpPut("{id}/expire")]
        public async Task<IActionResult> ExpirePolicy(int id)
        {
            var result = await statusservice.ExpirePolicyAsync(id);
            if (!result)
            {
                return NotFound("Policy not found");
            }
            return Ok("Policy expired successfully");
        }
    }
}
