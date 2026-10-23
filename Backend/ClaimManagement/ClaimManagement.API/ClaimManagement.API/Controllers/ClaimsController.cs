using ClaimManagement.Application.DTO;
using ClaimManagement.Application.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ClaimManagement.API.Controllers
{
    [Route("api/[controller]")]
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

        [HttpPost]
        public async Task<IActionResult> CreateClaim(CreateClaimDto dto)
        {
            var error = await eligibilityRepository.CheckEligibilityAsync(dto);
            if (error != "")
                return BadRequest(error);

            var claim = await repository.AddClaimAsync(dto);
            return Ok(claim);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClaims()
        {
            var claims = await repository.GetAllClaimsAsync();
            return Ok(claims);
        }

        [HttpGet("{claimId:long}")]
        public async Task<IActionResult> GetClaimById(long claimId)
        {
            var claim = await repository.GetClaimByIdAsync(claimId);
            if (claim == null)
                return NotFound("Claim not found.");

            return Ok(claim);
        }

        [HttpPut("{claimId:long}")]
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

        [HttpDelete("{claimId:long}")]
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