
using UnderwritingMS.Application.DTO;

namespace UnderwritingMS.Application.Interface
{
    public interface IUnderwritingService
    {
        Task CreateCase(createUnderwritingDTO dto);

        Task<List<underwriting_caseDTO>> FetchAllCases();

        Task<underwriting_caseDTO?> FindCaseById(int id);

        Task UpdateCase(int id, createUnderwritingDTO dto);
    }
}
