
namespace Support.Application.DTOs
{
  public class SupportTicketDto
  {
    public int TicketId { get; set; }

    public int CustomerId { get; set; }

    public int CategoryId { get; set; }

    public string Subject { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public string Priority { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? ClosedAt { get; set; }
  }
}
