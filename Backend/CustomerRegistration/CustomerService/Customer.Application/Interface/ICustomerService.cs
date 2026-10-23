using CustomerRegistration.Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerRegistration.Application.Interface
{
    public interface ICustomerService
    {
        Task<string> AddCustomer(CreateCustomerDto dto);
        Task<List<CustomerResponseDto>> FetchCustomers();
        Task<CustomerResponseDto?> FindCustomerById(int id);
        Task<string> UpdateCustomer(int id, UpdateCustomerDto dto);
        Task<string> DeleteCustomer(int id);
        Task<List<CustomerResponseDto>> SearchCustomers(string searchText);
    }
}
