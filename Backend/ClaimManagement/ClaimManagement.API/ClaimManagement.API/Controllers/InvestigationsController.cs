using ClaimManagement.Application.DTO;
using ClaimManagement.Application.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ClaimManagement.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class InvestigationsController : ControllerBase
    {
        IInvestigationRepository repository;

        public InvestigationsController(IInvestigationRepository repository)
        {
            this.repository = repository;
        }

        [HttpPost("claims/{claimId:int}/investigation")]
        public async Task<IActionResult> AssignInvestigator(int claimId, CreateInvestigationDto dto)
        {
            if (claimId <= 0)
                return BadRequest("Invalid claim ID.");

            var investigation = await repository.AssignInvestigatorAsync(claimId, dto);
            return Ok(investigation);
        }

        [HttpPut("investigations/{id:int}")]
        public async Task<IActionResult> SubmitInvestigationReport(int id, InvestigationReportDto dto)
        {
            var investigation = await repository.SubmitInvestigationReportAsync(id, dto);
            if (investigation == null)
                return NotFound("Investigation not found.");

            return Ok(investigation);
        }
    }
}