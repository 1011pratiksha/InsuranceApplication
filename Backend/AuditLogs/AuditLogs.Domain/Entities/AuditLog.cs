
using System;
using System.ComponentModel.DataAnnotations;

namespace AuditLogs.Domain.Entities
{
  public class AuditLog
  {
    [Key]
    public int AuditLogId { get; set; }

    [Required]
    public int CustomerId { get; set; }

    [Required]
    [StringLength(50)]
    public string Action { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string EntityName { get; set; } = string.Empty;

    [Required]
    public int EntityId { get; set; }

    [StringLength(500)]
    public string? Description { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }
  }
}
