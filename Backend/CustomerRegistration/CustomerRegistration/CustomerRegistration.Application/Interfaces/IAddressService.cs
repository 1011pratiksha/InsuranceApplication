using CustomerRegistration.Application.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerRegistration.Application.Interfaces
{
    public interface IAddressService
    {
        Task<string> AddAddress(int customerId, CreateAddressDto dto);
        Task<List<CustomerAddressDto>> FetchAddressesByCustomerId(int customerId);
        Task<CustomerAddressDto?> FindAddressById(int id);
        Task<string> UpdateAddress(int id, CreateAddressDto dto);
        Task<string> DeleteAddress(int id);
    }
}
