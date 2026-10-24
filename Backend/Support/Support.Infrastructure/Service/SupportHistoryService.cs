
using Microsoft.EntityFrameworkCore;
using Support.Application.DTOs;
using Support.Application.Interfaces;
using Support.Infrastructure.Data;

namespace Support.Infrastructure.Services
{
  public class SupportHistoryService : ISupportHistoryService
  {
    private readonly AppDbContext db;

    public SupportHistoryService(AppDbContext db)
    {
      this.db = db;
    }

    public async Task<List<SupportHistoryDto>> GetAllHistory()
    {
      return await db.SupportHistories
          .Select(h => new SupportHistoryDto
          {
            HistoryId = h.HistoryId,
            TicketId = h.TicketId,
            Action = h.Action,
            Remarks = h.Remarks,
            ChangedBy = h.ChangedBy,
            ChangedAt = h.ChangedAt
          })
          .ToListAsync();
    }

    public async Task<SupportHistoryDto?> GetHistoryById(int id)
    {
      var history = await db.SupportHistories.FindAsync(id);

      if (history == null)
      {
        return null;
      }

      return new SupportHistoryDto
      {
        HistoryId = history.HistoryId,
        TicketId = history.TicketId,
        Action = history.Action,
        Remarks = history.Remarks,
        ChangedBy = history.ChangedBy,
        ChangedAt = history.ChangedAt
      };
    }

    public async Task<List<SupportHistoryDto>> GetHistoryByTicketId(int ticketId)
    {
      return await db.SupportHistories
          .Where(h => h.TicketId == ticketId)
          .Select(h => new SupportHistoryDto
          {
            HistoryId = h.HistoryId,
            TicketId = h.TicketId,
            Action = h.Action,
            Remarks = h.Remarks,
            ChangedBy = h.ChangedBy,
            ChangedAt = h.ChangedAt
          })
          .ToListAsync();
    }

    public async Task<SupportHistoryDto> CreateHistory(SupportHistoryDto history)
    {
      var newHistory = new Support.Domain.Entities.SupportHistory
      {
        TicketId = history.TicketId,
        Action = history.Action,
        Remarks = history.Remarks,
        ChangedBy = history.ChangedBy,
        ChangedAt = DateTime.Now
      };

      db.SupportHistories.Add(newHistory);
      await db.SaveChangesAsync();

      history.HistoryId = newHistory.HistoryId;
      history.ChangedAt = newHistory.ChangedAt;

      return history;
    }

    public async Task<SupportHistoryDto?> UpdateHistory(int id, SupportHistoryDto history)
    {
      var existingHistory = await db.SupportHistories.FindAsync(id);

      if (existingHistory == null)
      {
        return null;
      }

      existingHistory.TicketId = history.TicketId;
      existingHistory.Action = history.Action;
      existingHistory.Remarks = history.Remarks;
      existingHistory.ChangedBy = history.ChangedBy;

      await db.SaveChangesAsync();

      return new SupportHistoryDto
      {
        HistoryId = existingHistory.HistoryId,
        TicketId = existingHistory.TicketId,
        Action = existingHistory.Action,
        Remarks = existingHistory.Remarks,
        ChangedBy = existingHistory.ChangedBy,
        ChangedAt = existingHistory.ChangedAt
      };
    }

    public async Task<bool> DeleteHistory(int id)
    {
      var history = await db.SupportHistories.FindAsync(id);

      if (history == null)
      {
        return false;
      }

      db.SupportHistories.Remove(history);
      await db.SaveChangesAsync();

      return true;
    }
  }
}
