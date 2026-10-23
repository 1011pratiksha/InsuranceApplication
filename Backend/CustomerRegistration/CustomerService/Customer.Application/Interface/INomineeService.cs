using CustomerRegistration.Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerRegistration.Application.Interface
{
    public interface INomineeService
    {
        Task<string> AddNominee(int customerId, NomineeDto dto);
        Task<List<NomineeDto>> FetchNominees(int customerId);
        Task<string> UpdateNominee(int nomineeId, NomineeDto dto);
        Task<string> DeleteNominee(int nomineeId);
    }
}
