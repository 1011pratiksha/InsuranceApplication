
using ProductManagementMS.Application.DTO;

namespace ProductManagementMS.Application.Interface
{
    public interface IBenefitService
    {
        Task AddBenefit(int productId, benefit_masterDTO dto);

        Task<List<benefit_masterDTO>> FetchBenefitsByProductId(int productId);

        Task UpdateBenefit(int benefitId, benefit_masterDTO dto);

        Task DeleteBenefit(int benefitId);
    }
}
