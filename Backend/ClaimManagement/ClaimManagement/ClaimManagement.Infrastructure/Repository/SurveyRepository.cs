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
            survey.ClaimId = claimId;
            survey.AssignedDate = DateTime.UtcNow;
            survey.SurveyStatus = "Assigned";

            await context.SurveyAssignments.AddAsync(survey);
            await context.SaveChangesAsync();

            return mapper.Map<SurveyResponseDto>(survey);
        }

        public async Task<List<SurveyResponseDto>> GetSurveysByClaimIdAsync(int claimId)
        {
            var data = await context.SurveyAssignments
                .Where(s => s.ClaimId == claimId)
                .OrderByDescending(s => s.AssignedDate)
                .ToListAsync();

            return mapper.Map<List<SurveyResponseDto>>(data);
        }

        // Returns null if the survey is not found
        public async Task<SurveyResponseDto?> SubmitSurveyReportAsync(int surveyId, SurveyReportDto dto)
        {
            var survey = await context.SurveyAssignments.FirstOrDefaultAsync(s => s.SurveyId == surveyId);
            if (survey == null)
                return null;

            mapper.Map(dto, survey);
            survey.SurveyStatus = "Completed";
            await context.SaveChangesAsync();

            return mapper.Map<SurveyResponseDto>(survey);
        }
    }
}