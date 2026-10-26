
using UnderwritingMS.Application.DTO;

namespace UnderwritingMS.Application.Interface
{
    public interface IKycClient
    {
        Task<kycResponseDTO?> GetKycStatus(int customerId);
    }
}
