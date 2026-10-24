
using System.Net.Http.Json;
using Support.Application.Interfaces;

namespace Support.Infrastructure.Services
{
  public class AuditLogApiService : IAuditLogApiService
  {
    private readonly HttpClient httpClient;

    public AuditLogApiService(HttpClient httpClient)
    {
      this.httpClient = httpClient;
    }

    public async Task LogActionAsync(
        int customerId,
        string action,
        string entityName,
        int entityId,
        string? description)
    {
      var request = new
      {
        CustomerId = customerId,
        Action = action,
        EntityName = entityName,
        EntityId = entityId,
        Description = description
      };

      var response = await httpClient.PostAsJsonAsync(
          "api/AuditLogs/log",
          request);

      response.EnsureSuccessStatusCode();
    }
  }
}
