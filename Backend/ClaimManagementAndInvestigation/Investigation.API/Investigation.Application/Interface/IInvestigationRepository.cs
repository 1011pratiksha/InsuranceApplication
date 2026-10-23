using Investigation.Domain.Entities;

namespace Investigation.Application.Interface
{
    public interface IInvestigationRepository
    {
        Task<InvestigationCase> AddAsync(InvestigationCase investigation);
        Task<InvestigationCase?> GetByIdAsync(int investigationId);
        Task UpdateAsync(InvestigationCase investigation);
    }
}