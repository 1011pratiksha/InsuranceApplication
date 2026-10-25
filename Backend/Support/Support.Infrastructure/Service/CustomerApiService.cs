
using System.Net;
using Support.Application.Interfaces;

namespace Support.Infrastructure.Services
{
  public class CustomerApiService : ICustomerApiService
  {
    private readonly HttpClient httpClient;

    public CustomerApiService(HttpClient httpClient)
    {
      this.httpClient = httpClient;
    }

    public async Task<bool> CustomerExists(int customerId)
    {
      try
      {
        var url = $"api/v1/customers/{customerId}";

        Console.WriteLine($"Customer API Base URL: {httpClient.BaseAddress}");
        Console.WriteLine($"Customer API Request: {url}");

        var response = await httpClient.GetAsync(url);
        var responseBody = await response.Content.ReadAsStringAsync();

        Console.WriteLine($"Customer API Status: {(int)response.StatusCode}");
        Console.WriteLine($"Customer API Response: {responseBody}");

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
          return false;
        }

        response.EnsureSuccessStatusCode();

        return true;
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Customer API Error: {ex}");
        throw;
      }
    }
  }
}
