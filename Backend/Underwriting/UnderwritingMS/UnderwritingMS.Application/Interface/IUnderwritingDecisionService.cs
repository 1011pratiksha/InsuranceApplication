
using UnderwritingMS.Application.DTO;

namespace UnderwritingMS.Application.Interface
{
    public interface IUnderwritingDecisionService
    {
        Task ApproveCase(int id, createUnderwritingDecisionDTO dto);

        Task RejectCase(int id, createUnderwritingDecisionDTO dto);

        Task ApplyLoading(int id, createUnderwritingDecisionDTO dto);

        Task<underwriting_decisionDTO?> GetDecision(int id);
    }
}
