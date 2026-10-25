
namespace Support.Application.DTOs
{
  public class SupportAttachmentDto
  {
    public int AttachmentId { get; set; }

    public int TicketId { get; set; }

    public string FileName { get; set; } = string.Empty;

    public string FilePath { get; set; } = string.Empty;

    public string? ContentType { get; set; }

    public DateTime UploadedAt { get; set; }
  }
}
