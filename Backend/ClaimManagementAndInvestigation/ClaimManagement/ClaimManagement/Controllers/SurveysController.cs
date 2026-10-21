using AutoMapper;
using ClaimManagement.Application.DTO;
using ClaimManagement.Application.Interface;
using ClaimManagement.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ClaimManagement.Controllers
{
    [ApiController]
    public class SurveysController : ControllerBase
    {
        private readonly ISurveyRepository surveyRepository;
        private readonly IClaimRepository claimRepository;
        private readonly IMapper mapper;

        public SurveysController(ISurveyRepository surveyRepository, IClaimRepository claimRepository, IMapper mapper)
        {
            this.surveyRepository = surveyRepository;
            this.claimRepository = claimRepository;
            this.mapper = mapper;
        }

        [HttpPost("api/claims/{claimId:int}/survey")]
        public async Task<IActionResult> AssignSurveyor(int claimId, CreateSurveyDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await claimRepository.ExistsAsync(claimId))
                return NotFound("Claim not found.");

            var survey = mapper.Map<SurveyAssignment>(dto);
            survey.ClaimId = claimId;
            survey.AssignedDate = DateTime.UtcNow;
            survey.SurveyStatus = "Assigned";

            var savedSurvey = await surveyRepository.AddAsync(survey);

            return Ok(savedSurvey);
        }

        [HttpGet("api/claims/{claimId:int}/survey")]
        public async Task<IActionResult> GetSurveyDetails(int claimId)
        {
            if (!await claimRepository.ExistsAsync(claimId))
                return NotFound("Claim not found.");

            var surveys = await surveyRepository.GetByClaimIdAsync(claimId);

            return Ok(surveys);
        }

        [HttpPut("api/surveys/{surveyId:int}")]
        public async Task<IActionResult> SubmitSurveyReport(int surveyId, SurveyReportDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var survey = await surveyRepository.GetByIdAsync(surveyId);

            if (survey == null)
                return NotFound("Survey assignment not found.");

            survey.InspectionDate = dto.InspectionDate;
            survey.ReportPath = dto.ReportPath;
            survey.Remarks = dto.Remarks;
            survey.SurveyStatus = "Completed";

            await surveyRepository.UpdateAsync(survey);

            return Ok(survey);
        }
    }
}