
using ProductManagementMS.Application.DTO;

namespace ProductManagementMS.Application.Interface
{
    public interface IRiderService
    {
        Task AddRider(int productId, rider_masterDTO dto);

        Task<List<rider_masterDTO>> FetchRidersByProductId(int productId);

        Task UpdateRider(int riderId, rider_masterDTO dto);

        Task DeleteRider(int riderId);
    }
}
