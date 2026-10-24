
using ProductManagementMS.Application.DTO;

namespace ProductManagementMS.Application.Interface
{
    public interface IProductCommissionService
    {
        Task AddCommission(int productId, product_commissionDTO dto);

        Task<product_commissionDTO?> FindCommissionByProductId(
            int productId);

        Task UpdateCommission(
            int commissionId, product_commissionDTO dto);

        Task DeleteCommission(int commissionId);
    }
}
