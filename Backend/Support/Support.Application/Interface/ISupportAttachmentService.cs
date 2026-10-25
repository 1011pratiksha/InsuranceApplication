
using Support.Application.DTOs;

namespace Support.Application.Interfaces
{
  public interface ISupportAttachmentService
  {
    Task<List<SupportAttachmentDto>> GetAllAttachments();

    Task<SupportAttachmentDto?> GetAttachmentById(int id);

    Task<List<SupportAttachmentDto>> GetAttachmentsByTicketId(int ticketId);

    Task<SupportAttachmentDto> CreateAttachment(SupportAttachmentDto attachment);

    Task<SupportAttachmentDto?> UpdateAttachment(int id, SupportAttachmentDto attachment);

    Task<bool> DeleteAttachment(int id);
  }
}
