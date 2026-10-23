using ClaimManagement.Application.DTO;
using ClaimManagement.Application.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ClaimManagement.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class SurveysController : ControllerBase
    {
        ISurveyRepository surveyRepository;
        IClaimRepository claimRepository;

        public SurveysController(ISurveyRepository surveyRepository, IClaimRepository claimRepository)
        {
            this.surveyRepository = surveyRepository;
            this.claimRepository = claimRepository;
        }

        [HttpPost("claims/{claimId:int}/survey")]
        public async Task<IActionResult> AssignSurveyor(int claimId, CreateSurveyDto dto)
        {
            var claim = await claimRepository.GetClaimByIdAsync(claimId);
            if (claim == null)
                return NotFound("Claim not found.");

            var survey = await surveyRepository.AssignSurveyorAsync(claimId, dto);
            return Ok(survey);
        }

        [HttpGet("claims/{claimId:int}/survey")]
        public async Task<IActionResult> GetSurveyDetails(int claimId)
        {
            var claim = await claimRepository.GetClaimByIdAsync(claimId);
            if (claim == null)
                return NotFound("Claim not found.");

            var surveys = await surveyRepository.GetSurveysByClaimIdAsync(claimId);
            return Ok(surveys);
        }

        [HttpPut("surveys/{surveyId:int}")]
        public async Task<IActionResult> SubmitSurveyReport(int surveyId, SurveyReportDto dto)
        {
            var survey = await surveyRepository.SubmitSurveyReportAsync(surveyId, dto);
            if (survey == null)
                return NotFound("Survey assignment not found.");

            return Ok(survey);
        }
    }
}