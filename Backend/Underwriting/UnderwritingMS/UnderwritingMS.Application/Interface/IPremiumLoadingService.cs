
using UnderwritingMS.Application.DTO;

namespace UnderwritingMS.Application.Interface
{
    public interface IPremiumLoadingService
    {
        Task<premiumLoadingDTO> CalculateLoading(int id);
    }
}
