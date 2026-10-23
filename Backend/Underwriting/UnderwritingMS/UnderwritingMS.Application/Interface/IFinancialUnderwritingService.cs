
using UnderwritingMS.Application.DTO;

namespace UnderwritingMS.Application.Interface
{
    public interface IFinancialUnderwritingService
    {
        Task AddFinancialDetails(int id, financial_underwritingDTO dto);

        Task<financial_underwritingDTO?> GetFinancialDetails(int id);

        Task UpdateFinancialDetails(int id, financial_underwritingDTO dto);
    }
}
