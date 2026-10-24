
using CustomerRegistration.Application.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerRegistration.Application.Interfaces
{
  public interface ICustomerService
  {
    Task<int?> AddCustomer(CreateCustomerDto dto);
    Task<List<CustomerResponseDto>> FetchCustomers();
    Task<CustomerResponseDto?> FindCustomerById(int id);
    Task<string> UpdateCustomer(int id, UpdateCustomerDto dto);
    Task<string> DeleteCustomer(int id);
    Task<List<CustomerResponseDto>> SearchCustomers(string searchText);
  }
}
