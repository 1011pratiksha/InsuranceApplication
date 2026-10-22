using ClaimManagement.Application.DTO;

namespace ClaimManagement.Application.Interface
{
    public interface ISurveyRepository
    {
        Task<SurveyResponseDto> AssignSurveyorAsync(int claimId, CreateSurveyDto dto);
        Task<List<SurveyResponseDto>> GetSurveysByClaimIdAsync(int claimId);
        Task<SurveyResponseDto?> SubmitSurveyReportAsync(int surveyId, SurveyReportDto dto);
    }
}