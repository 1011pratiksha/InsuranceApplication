
using ProductManagementMS.Application.DTO;

namespace ProductManagementMS.Application.Interface
{
    public interface ICoverageService
    {
        Task AddCoverage(int productId, coverage_masterDTO dto);

        Task<List<coverage_masterDTO>> FetchCoveragesByProductId(int productId);

        Task UpdateCoverage(int coverageId, coverage_masterDTO dto);

        Task DeleteCoverage(int coverageId);
    }
}
