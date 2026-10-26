using ClaimManagement.Application.DTO;
using ClaimManagement.Application.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClaimManagement.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class ClaimAssessmentController : ControllerBase
    {
        IClaimAssessmentRepository assessmentRepository;
        IClaimRepository claimRepository;

        public ClaimAssessmentController(IClaimAssessmentRepository assessmentRepository, IClaimRepository claimRepository)
        {
            this.assessmentRepository = assessmentRepository;
            this.claimRepository = claimRepository;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("admin/claims/{claimId:long}/assessment")]
        public async Task<IActionResult> AddAssessment(long claimId, CreateAssessmentDto dto)
        {
            var claim = await claimRepository.GetClaimByIdAsync(claimId);
            if (claim == null)
                return NotFound("Claim not found.");

            var assessment = await assessmentRepository.AddAssessmentAsync(claimId, dto);
            return Ok(assessment);
        }

        [Authorize(Roles = "Admin,Customer")]
        [HttpGet("claims/{claimId:long}/assessment")]
        public async Task<IActionResult> GetAssessment(long claimId)
        {
            var claim = await claimRepository.GetClaimByIdAsync(claimId);
            if (claim == null)
                return NotFound("Claim not found.");

            var assessment = await assessmentRepository.GetAssessmentByClaimIdAsync(claimId);
            if (assessment == null)
                return NotFound("Assessment not found.");

            return Ok(assessment);
        }
    }
}
