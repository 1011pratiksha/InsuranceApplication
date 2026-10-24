using Investigation.Application.DTO;

namespace Investigation.Application.Interface
{
    public interface IInvestigationRepository
    {
        Task<InvestigationResponseDto> AssignInvestigatorAsync(int claimId, CreateInvestigationDto dto);
        Task<InvestigationResponseDto?> SubmitInvestigationReportAsync(int id, InvestigationReportDto dto);
    }
}