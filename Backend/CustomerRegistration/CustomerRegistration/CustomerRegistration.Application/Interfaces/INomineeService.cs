using CustomerRegistration.Application.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerRegistration.Application.Interfaces
{
    public interface INomineeService
    {
        Task<string> AddNominee(int customerId, CreateNomineeDto dto);
        Task<List<NomineeResponseDto>> FetchNomineesByCustomerId(int customerId);
        Task<NomineeResponseDto?> FindNomineeById(int id);
        Task<string> UpdateNominee(int id, CreateNomineeDto dto);
        Task<string> DeleteNominee(int id);
    }
}
