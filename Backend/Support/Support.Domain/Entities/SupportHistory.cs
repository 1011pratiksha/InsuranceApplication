
using System;
using System.ComponentModel.DataAnnotations;

namespace Support.Domain.Entities
{
  public class SupportHistory
  {
    [Key]
    public int HistoryId { get; set; }

    [Required]
    public int TicketId { get; set; }

    [Required]
    [StringLength(50)]
    public string Action { get; set; } = string.Empty;

    [StringLength(500)]
    public string? Remarks { get; set; }

    [Required]
    [StringLength(50)]
    public string ChangedBy { get; set; } = string.Empty;

    public DateTime ChangedAt { get; set; } = DateTime.Now;
  }
}
