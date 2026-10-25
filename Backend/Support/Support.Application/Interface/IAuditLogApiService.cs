
namespace Support.Application.Interfaces
{
  public interface IAuditLogApiService
  {
    Task LogActionAsync(
        int customerId,
        string action,
        string entityName,
        int entityId,
        string? description);
  }
}
