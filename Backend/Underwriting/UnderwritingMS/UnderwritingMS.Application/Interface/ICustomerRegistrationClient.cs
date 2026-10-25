
using UnderwritingMS.Application.DTO;

namespace UnderwritingMS.Application.Interface
{
    public interface ICustomerRegistrationClient
    {
        Task<customerRegistrationResponseDTO?> GetCustomerById(int customerId);
    }
}
