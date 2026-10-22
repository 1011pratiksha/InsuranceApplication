using ClaimManagement.Application.Interface;
using ClaimManagement.Domain.Entities;
using ClaimManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ClaimManagement.Infrastructure.Repository
{
    public class SurveyRepository : ISurveyRepository
    {
        private readonly AppDbContext context;

        public SurveyRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<SurveyAssignment> AddAsync(SurveyAssignment survey)
        {
            context.SurveyAssignments.Add(survey);
            await context.SaveChangesAsync();
            return survey;
        }

        public async Task<List<SurveyAssignment>> GetByClaimIdAsync(int claimId)
        {
            return await context.SurveyAssignments
                .AsNoTracking()
                .Where(s => s.ClaimId == claimId)
                .OrderByDescending(s => s.AssignedDate)
                .ToListAsync();
        }

        public async Task<SurveyAssignment?> GetByIdAsync(int surveyId)
        {
            return await context.SurveyAssignments
                .FirstOrDefaultAsync(s => s.SurveyId == surveyId);
        }

        public async Task UpdateAsync(SurveyAssignment survey)
        {
            context.SurveyAssignments.Update(survey);
            await context.SaveChangesAsync();
        }
    }
}