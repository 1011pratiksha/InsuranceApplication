using ClaimManagement.Application.Interface;
using ClaimManagement.Domain.Entities;
using ClaimManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ClaimManagement.Infrastructure.Repository
{
    public class SurveyRepository : ISurveyRepository
    {
        private readonly AppDbContext db;

        public SurveyRepository(AppDbContext db)
        {
            this.db = db;
        }

        public async Task<SurveyAssignment> AddAsync(SurveyAssignment survey)
        {
            db.SurveyAssignments.Add(survey);
            await db.SaveChangesAsync();
            return survey;
        }

        public async Task<List<SurveyAssignment>> GetByClaimIdAsync(int claimId)
        {
            return await db.SurveyAssignments
                .AsNoTracking()
                .Where(s => s.ClaimId == claimId)
                .OrderByDescending(s => s.AssignedDate)
                .ToListAsync();
        }

        public async Task<SurveyAssignment?> GetByIdAsync(int surveyId)
        {
            return await db.SurveyAssignments
                .FirstOrDefaultAsync(s => s.SurveyId == surveyId);
        }

        public async Task UpdateAsync(SurveyAssignment survey)
        {
            db.SurveyAssignments.Update(survey);
            await db.SaveChangesAsync();
        }
    }
}