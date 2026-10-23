using AutoMapper;
using ClaimManagement.Application.DTO;
using ClaimManagement.Application.Interface;
using ClaimManagement.Domain.Entities;
using ClaimManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ClaimManagement.Infrastructure.Repository
{
    public class SurveyRepository : ISurveyRepository
    {
        AppDbContext context;
        IMapper mapper;

        public SurveyRepository(AppDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<SurveyResponseDto> AssignSurveyorAsync(int claimId, CreateSurveyDto dto)
        {
            var survey = mapper.Map<SurveyAssignment>(dto);
            survey.claim_id = claimId;
            survey.assigned_date = DateTime.UtcNow;
            survey.survey_status = "Assigned";

            await context.SurveyAssignments.AddAsync(survey);
            await context.SaveChangesAsync();

            return mapper.Map<SurveyResponseDto>(survey);
        }

        public async Task<List<SurveyResponseDto>> GetSurveysByClaimIdAsync(int claimId)
        {
            var data = await context.SurveyAssignments
                .Where(s => s.claim_id == claimId)
                .OrderByDescending(s => s.assigned_date)
                .ToListAsync();

            return mapper.Map<List<SurveyResponseDto>>(data);
        }

        // Returns null if the survey is not found
        public async Task<SurveyResponseDto?> SubmitSurveyReportAsync(int surveyId, SurveyReportDto dto)
        {
            var survey = await context.SurveyAssignments.FirstOrDefaultAsync(s => s.survey_id == surveyId);
            if (survey == null)
                return null;

            mapper.Map(dto, survey);
            survey.survey_status = "Completed";
            await context.SaveChangesAsync();

            return mapper.Map<SurveyResponseDto>(survey);
        }
    }
}