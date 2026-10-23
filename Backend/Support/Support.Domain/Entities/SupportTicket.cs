
using System;
using System.ComponentModel.DataAnnotations;

namespace Support.Domain.Entities
{
  public class SupportTicket
  {
    [Key]
    public int TicketId { get; set; }

    [Required]
    public int CustomerId { get; set; }

    [Required]
    public int CategoryId { get; set; }

    [Required]
    [StringLength(100)]
    public string Subject { get; set; } = string.Empty;

    [Required]
    [StringLength(1000)]
    public string Description { get; set; } = string.Empty;

    [Required]
    [StringLength(20)]
    public string Status { get; set; } = "Open";

    [Required]
    [StringLength(20)]
    public string Priority { get; set; } = "Medium";

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime? UpdatedAt { get; set; }

    public DateTime? ClosedAt { get; set; }
  }
}
