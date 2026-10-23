
using AuditLogs.Application.DTO;
namespace AuditLogs.Application.Interfaces
{
  public interface IAuditLogService
  {
    Task<List<AuditLogDto>> GetAllAsync();
    Task<AuditLogDto?> GetByIdAsync(int id);
    Task<List<AuditLogDto>> GetByCustomerIdAsync(int customerId);
    Task<AuditLogDto> AddAsync(AuditLogDto auditLogDto);
    Task<bool> UpdateAsync(int id, AuditLogDto auditLogDto);
    Task<bool> DeleteAsync(int id);

    Task LogActionAsync(
        int customerId,
        string action,
        string entityName,
        int entityId,
        string? description);

  }
}
