
using System;
using System.ComponentModel.DataAnnotations;

namespace Support.Domain.Entities
{
  public class SupportAttachment
  {
    [Key]
    public int AttachmentId { get; set; }

    [Required]
    public int TicketId { get; set; }

    [Required]
    [StringLength(200)]
    public string FileName { get; set; } = string.Empty;

    [Required]
    [StringLength(500)]
    public string FilePath { get; set; } = string.Empty;

    [StringLength(50)]
    public string? ContentType { get; set; }

    public DateTime UploadedAt { get; set; } = DateTime.Now;
  }
}
