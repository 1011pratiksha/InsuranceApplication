using System;
using System.Collections.Generic;
using System.Text;
using CustomerRegistration.Application.DTO;

namespace CustomerRegistration.Application.Interface
{
    public interface IAddressService
    {
        Task<string> AddAddress(int customerId, CustomerAddressDto dto);
        Task<List<CustomerAddressDto>> FetchAddresses(int customerId);
        Task<string> UpdateAddress(int addressId, CustomerAddressDto dto);
        Task<string> DeleteAddress(int addressId);
    }
}
