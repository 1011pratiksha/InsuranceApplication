
using UnderwritingMS.Application.DTO;

namespace UnderwritingMS.Application.Interface
{
    public interface IMedicalUnderwritingService
    {
        Task AddMedicalDetails(int id, medical_underwritingDTO dto);

        Task<medical_underwritingDTO?> GetMedicalDetails(int id);

        Task UpdateMedicalDetails(int id, medical_underwritingDTO dto);
    }
}
