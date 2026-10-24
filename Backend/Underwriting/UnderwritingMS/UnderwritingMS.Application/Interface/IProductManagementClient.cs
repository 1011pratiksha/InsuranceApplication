
using UnderwritingMS.Application.DTO;

namespace UnderwritingMS.Application.Interface
{
    public interface IProductManagementClient
    {
        Task<productManagementDTO?> GetProductById(int id);
    }
}
