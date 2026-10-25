
using Support.Application.DTOs;

namespace Support.Application.Interfaces
{
  public interface ISupportResponseService
  {
    Task<List<SupportResponseDto>> GetAllResponses();

    Task<SupportResponseDto?> GetResponseById(int id);

    Task<List<SupportResponseDto>> GetResponsesByTicketId(int ticketId);

    Task<SupportResponseDto> CreateResponse(SupportResponseDto response);

    Task<SupportResponseDto?> UpdateResponse(int id, SupportResponseDto response);

    Task<bool> DeleteResponse(int id);
  }
}
