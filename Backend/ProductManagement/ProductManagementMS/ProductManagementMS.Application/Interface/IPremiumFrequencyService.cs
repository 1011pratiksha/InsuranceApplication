
using ProductManagementMS.Application.DTO;

namespace ProductManagementMS.Application.Interface
{
    public interface IPremiumFrequencyService
    {
        Task AddFrequency(premium_frequencyDTO dto);

        Task<List<premium_frequencyDTO>> FetchAllFrequencies();

        Task<premium_frequencyDTO?> FindFrequencyById(int id);

        Task UpdateFrequency(int id, premium_frequencyDTO dto);

        Task DeactivateFrequency(int id);
    }
}
