
namespace AuditLogs.Application.DTO
{
  public class AuditLogRequestDto
  {
    public int CustomerId { get; set; }

    public string Action { get; set; } = string.Empty;

    public string EntityName { get; set; } = string.Empty;

    public int EntityId { get; set; }

    public string? Description { get; set; }
  }
}
