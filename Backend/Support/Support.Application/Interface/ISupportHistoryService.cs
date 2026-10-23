
using Support.Application.DTOs;

namespace Support.Application.Interfaces
{
  public interface ISupportHistoryService
  {
    Task<List<SupportHistoryDto>> GetAllHistory();

    Task<SupportHistoryDto?> GetHistoryById(int id);

    Task<List<SupportHistoryDto>> GetHistoryByTicketId(int ticketId);

    Task<SupportHistoryDto> CreateHistory(SupportHistoryDto history);

    Task<SupportHistoryDto?> UpdateHistory(int id, SupportHistoryDto history);

    Task<bool> DeleteHistory(int id);
  }
}
