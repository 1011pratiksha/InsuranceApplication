
namespace Support.Application.DTOs
{
  public class SupportResponseDto
  {
    public int ResponseId { get; set; }

    public int TicketId { get; set; }

    public string Message { get; set; } = string.Empty;

    public string RespondedBy { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
  }
}
