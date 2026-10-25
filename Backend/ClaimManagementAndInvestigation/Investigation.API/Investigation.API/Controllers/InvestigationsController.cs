using AutoMapper;
using Investigation.Application.DTO;
using Investigation.Application.Interface;
using Investigation.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Investigation.Controllers
{
    [ApiController]
    public class InvestigationsController : ControllerBase
    {
        private readonly IInvestigationRepository repository;
        private readonly IMapper mapper;

        public InvestigationsController(IInvestigationRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpPost("api/claims/{claimId:int}/investigation")]
        public async Task<IActionResult> AssignInvestigator(int claimId, CreateInvestigationDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (claimId <= 0)
                return BadRequest("Invalid claim ID.");

            var investigation = mapper.Map<InvestigationCase>(dto);
            investigation.ClaimId = claimId;
            investigation.CreatedDate = DateTime.UtcNow;
            investigation.InvestigationStatus = "Pending";

            if (dto.InvestigatorId.HasValue)
                investigation.AssignedDate = DateTime.UtcNow;

            var savedInvestigation = await repository.AddAsync(investigation);

            return Ok(savedInvestigation);
        }

        [HttpPut("api/investigations/{id:int}")]
        public async Task<IActionResult> SubmitInvestigationReport(int id, InvestigationReportDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var investigation = await repository.GetByIdAsync(id);

            if (investigation == null)
                return NotFound("Investigation not found.");

            investigation.ReportPath = dto.ReportPath;
            investigation.InvestigationStatus = "Completed";
            investigation.CompletedDate = DateTime.UtcNow;

            await repository.UpdateAsync(investigation);

            return Ok(investigation);
        }
    }
}