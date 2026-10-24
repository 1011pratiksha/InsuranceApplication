
using System;
using System.ComponentModel.DataAnnotations;

namespace Support.Domain.Entities
{
  public class SupportResponse
  {
    [Key]
    public int ResponseId { get; set; }

    [Required]
    public int TicketId { get; set; }

    [Required]
    [StringLength(1000)]
    public string Message { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string RespondedBy { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.Now;
  }
}
