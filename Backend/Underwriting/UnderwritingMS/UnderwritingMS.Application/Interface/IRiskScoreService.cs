
using UnderwritingMS.Application.DTO;

namespace UnderwritingMS.Application.Interface
{
    public interface IRiskScoreService
    {
        Task CalculateRiskScore(int id, calculateRiskScoreDTO dto);

        Task<risk_scoreDTO?> GetRiskScore(int id);
    }
}
