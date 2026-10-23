
namespace Support.Application.DTOs
{
  public class SupportHistoryDto
  {
    public int HistoryId { get; set; }

    public int TicketId { get; set; }

    public string Action { get; set; } = string.Empty;

    public string? Remarks { get; set; }

    public string ChangedBy { get; set; } = string.Empty;

    public DateTime ChangedAt { get; set; }
  }
}
