
using AutoMapper;
using ClaimManagement.Application.DTO;
using ClaimManagement.Application.Interface;
using ClaimManagement.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ClaimManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClaimsController : ControllerBase
    {
        private readonly IClaimRepository repository;
        private readonly IClaimEligibilityRepository eligibilityRepository;
        private readonly IMapper mapper;

        public ClaimsController(IClaimRepository repository, IClaimEligibilityRepository eligibilityRepository, IMapper mapper)
        {
            this.repository = repository;
            this.eligibilityRepository = eligibilityRepository;
            this.mapper = mapper;
        }

        // Register a new claim
        [HttpPost]
        public async Task<IActionResult> CreateClaim(CreateClaimDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (dto.IncidentDate == default || dto.IncidentDate.Date > DateTime.UtcNow.Date)
            {
                return BadRequest("Invalid incident date.");
            }

            if (!await eligibilityRepository.CustomerExistsAsync(dto.CustomerId))
            {
                return BadRequest("Customer does not exist.");
            }

            if (!await eligibilityRepository.PolicyBelongsToCustomerAsync(dto.PolicyId, dto.CustomerId))
            {
                return BadRequest("Policy does not belong to this customer.");
            }

            if (!await eligibilityRepository.IsPolicyActiveAsync(dto.PolicyId, dto.IncidentDate))
            {
                return BadRequest("Policy is not active or does not cover the incident date.");
            }

            if (!await eligibilityRepository.HasValidCoverageAsync(dto.PolicyId, dto.IncidentDate, dto.EstimatedLoss))
            {
                return BadRequest("Valid coverage was not found.");
            }

            if (!await eligibilityRepository.HasEligiblePremiumAsync(dto.PolicyId))
            {
                return BadRequest("Premium eligibility requirements are not satisfied.");
            }

            var claim = mapper.Map<Claim>(dto);
            claim.ClaimNumber = $"CLM-{Guid.NewGuid():N}"[..16].ToUpperInvariant();
            claim.ClaimDate = DateTime.UtcNow;
            claim.ClaimStatus = "Registered";

            var savedClaim = await repository.AddAsync(claim);

            return Ok(mapper.Map<ClaimResponseDto>(savedClaim));
        }

        // Get all claims
        [HttpGet]
        public async Task<IActionResult> GetAllClaims()
        {
            var claims = await repository.GetAllAsync();
            return Ok(mapper.Map<List<ClaimResponseDto>>(claims));
        }

        // Get claim by ID
        [HttpGet("{claimId:long}")]
        public async Task<IActionResult> GetClaimById(long claimId)
        {
            var claim = await repository.GetByIdAsync(claimId);

            if (claim == null)
            {
                return NotFound("Claim not found.");
            }

            return Ok(mapper.Map<ClaimResponseDto>(claim));
        }

        // Update a claim
        [HttpPut("{claimId:long}")]
        public async Task<IActionResult> UpdateClaim(long claimId, UpdateClaimDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var claim = await repository.GetByIdAsync(claimId);

            if (claim == null)
            {
                return NotFound("Claim not found.");
            }

            if (claim.ClaimStatus != "Registered")
            {
                return BadRequest("Only registered claims can be updated.");
            }

            if (dto.IncidentDate == default || dto.IncidentDate.Date > DateTime.UtcNow.Date)
            {
                return BadRequest("Invalid incident date.");
            }

            mapper.Map(dto, claim);

            await repository.UpdateAsync(claim);

            return Ok(mapper.Map<ClaimResponseDto>(claim));
        }

        // Delete a registered claim
        [HttpDelete("{claimId:long}")]
        public async Task<IActionResult> DeleteClaim(long claimId)
        {
            var claim = await repository.GetByIdAsync(claimId);

            if (claim == null)
            {
                return NotFound("Claim not found.");
            }

            if (claim.ClaimStatus != "Registered")
            {
                return BadRequest("Only registered claims can be deleted.");
            }

            await repository.DeleteAsync(claimId);

            return Ok("Claim deleted successfully.");
        }
    }
}

