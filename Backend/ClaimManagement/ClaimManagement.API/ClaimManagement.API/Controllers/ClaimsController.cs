using ClaimManagement.Application.DTO;
using ClaimManagement.Application.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClaimManagement.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class ClaimsController : ControllerBase
    {
        IClaimRepository repository;
        IClaimEligibilityRepository eligibilityRepository;

        public ClaimsController(IClaimRepository repository, IClaimEligibilityRepository eligibilityRepository)
        {
            this.repository = repository;
            this.eligibilityRepository = eligibilityRepository;
        }

        [Authorize(Roles = "Customer")]
        [HttpPost("customer/claims")]
        public async Task<IActionResult> CreateClaim(CreateClaimDto dto)
        {
            var error = await eligibilityRepository.CheckEligibilityAsync(dto);
            if (error != "")
                return BadRequest(error);

            var claim = await repository.AddClaimAsync(dto);
            return Ok(claim);
        }

        [Authorize(Roles = "Customer,Admin")]
        [HttpGet("claims")]
        public async Task<IActionResult> GetAllClaims()
        {
            var claims = await repository.GetAllClaimsAsync();
            return Ok(claims);
        }

        [Authorize(Roles = "Customer,Admin")]
        [HttpGet("claims/{claimId:long}")]
        public async Task<IActionResult> GetClaimById(long claimId)
        {
            var claim = await repository.GetClaimByIdAsync(claimId);
            if (claim == null)
                return NotFound("Claim not found.");

            return Ok(claim);
        }

        [Authorize(Roles = "Customer")]
        [HttpPut("customer/claims/{claimId:long}")]
        public async Task<IActionResult> UpdateClaim(long claimId, UpdateClaimDto dto)
        {
            var claim = await repository.GetClaimByIdAsync(claimId);
            if (claim == null)
                return NotFound("Claim not found.");

            var error = await repository.UpdateClaimAsync(claimId, dto);
            if (error != "")
                return BadRequest(error);

            var updatedClaim = await repository.GetClaimByIdAsync(claimId);
            return Ok(updatedClaim);
        }

        [Authorize(Roles = "Customer")]
        [HttpDelete("customer/claims/{claimId:long}")]
        public async Task<IActionResult> DeleteClaim(long claimId)
        {
            var claim = await repository.GetClaimByIdAsync(claimId);
            if (claim == null)
                return NotFound("Claim not found.");

            var error = await repository.DeleteClaimAsync(claimId);
            if (error != "")
                return BadRequest(error);

            return Ok("Claim deleted successfully.");
        }
    }
}