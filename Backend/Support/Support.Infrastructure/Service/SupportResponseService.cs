
using Microsoft.EntityFrameworkCore;
using Support.Application.DTOs;
using Support.Application.Interfaces;
using Support.Domain.Entities;
using Support.Infrastructure.Data;

namespace Support.Infrastructure.Services
{
  public class SupportResponseService : ISupportResponseService
  {
    private readonly AppDbContext db;

    public SupportResponseService(AppDbContext db)
    {
      this.db = db;
    }

    // 1. Get all responses
    public async Task<List<SupportResponseDto>> GetAllResponses()
    {
      return await db.SupportResponses
          .Select(r => new SupportResponseDto
          {
            ResponseId = r.ResponseId,
            TicketId = r.TicketId,
            Message = r.Message,
            RespondedBy = r.RespondedBy,
            CreatedAt = r.CreatedAt
          })
          .ToListAsync();
    }

    // 2. Get response by ID
    public async Task<SupportResponseDto?> GetResponseById(int id)
    {
      var r = await db.SupportResponses.FindAsync(id);

      if (r == null)
        return null;

      return new SupportResponseDto
      {
        ResponseId = r.ResponseId,
        TicketId = r.TicketId,
        Message = r.Message,
        RespondedBy = r.RespondedBy,
        CreatedAt = r.CreatedAt
      };
    }

    // 3. Get responses by ticket ID
    public async Task<List<SupportResponseDto>> GetResponsesByTicketId(int ticketId)
    {
      return await db.SupportResponses
          .Where(r => r.TicketId == ticketId)
          .Select(r => new SupportResponseDto
          {
            ResponseId = r.ResponseId,
            TicketId = r.TicketId,
            Message = r.Message,
            RespondedBy = r.RespondedBy,
            CreatedAt = r.CreatedAt
          })
          .ToListAsync();
    }

    // 4. Create response
    public async Task<SupportResponseDto> CreateResponse(SupportResponseDto response)
    {
      var newResponse = new SupportResponse
      {
        TicketId = response.TicketId,
        Message = response.Message,
        RespondedBy = response.RespondedBy,
        CreatedAt = DateTime.Now
      };

      db.SupportResponses.Add(newResponse);
      await db.SaveChangesAsync();

      response.ResponseId = newResponse.ResponseId;
      response.CreatedAt = newResponse.CreatedAt;

      return response;
    }

    // 5. Update response
    public async Task<SupportResponseDto?> UpdateResponse(int id, SupportResponseDto response)
    {
      var existingResponse = await db.SupportResponses.FindAsync(id);

      if (existingResponse == null)
        return null;

      existingResponse.TicketId = response.TicketId;
      existingResponse.Message = response.Message;
      existingResponse.RespondedBy = response.RespondedBy;

      await db.SaveChangesAsync();

      return await GetResponseById(id);
    }

    // 6. Delete response
    public async Task<bool> DeleteResponse(int id)
    {
      var response = await db.SupportResponses.FindAsync(id);

      if (response == null)
        return false;

      db.SupportResponses.Remove(response);
      await db.SaveChangesAsync();

      return true;
    }
  }
}
