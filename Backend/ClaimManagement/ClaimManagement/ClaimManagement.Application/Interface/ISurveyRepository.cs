using ClaimManagement.Domain.Entities;

namespace ClaimManagement.Application.Interface
{
    public interface ISurveyRepository
    {
        Task<SurveyAssignment> AddAsync(SurveyAssignment survey);
        Task<List<SurveyAssignment>> GetByClaimIdAsync(int claimId);
        Task<SurveyAssignment?> GetByIdAsync(int surveyId);
        Task UpdateAsync(SurveyAssignment survey);
    }
}