
namespace Support.Application.Interfaces
{
  public interface ICustomerApiService
  {
    Task<bool> CustomerExists(int customerId);
  }
}
