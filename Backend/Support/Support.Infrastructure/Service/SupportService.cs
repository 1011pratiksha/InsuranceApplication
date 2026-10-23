
using Microsoft.EntityFrameworkCore;
using Support.Application.DTOs;
using Support.Application.Interfaces;
using Support.Domain.Entities;
using Support.Infrastructure.Data;

namespace Support.Infrastructure.Services
{
  public class SupportService : ISupportService
  {
    private readonly AppDbContext db;

    public SupportService(AppDbContext db)
    {
      this.db = db;
    }

    public async Task<List<SupportTicketDto>> GetAllTickets()
    {
      return await db.SupportTickets
          .Select(t => new SupportTicketDto
          {
            TicketId = t.TicketId,
            CustomerId = t.CustomerId,
            CategoryId = t.CategoryId,
            Subject = t.Subject,
            Description = t.Description,
            Status = t.Status,
            Priority = t.Priority,
            CreatedAt = t.CreatedAt,
            UpdatedAt = t.UpdatedAt,
            ClosedAt = t.ClosedAt
          })
          .ToListAsync();
    }

    public async Task<SupportTicketDto?> GetTicketById(int id)
    {
      var t = await db.SupportTickets.FindAsync(id);

      if (t == null)
        return null;

      return new SupportTicketDto
      {
        TicketId = t.TicketId,
        CustomerId = t.CustomerId,
        CategoryId = t.CategoryId,
        Subject = t.Subject,
        Description = t.Description,
        Status = t.Status,
        Priority = t.Priority,
        CreatedAt = t.CreatedAt,
        UpdatedAt = t.UpdatedAt,
        ClosedAt = t.ClosedAt
      };
    }

    public async Task<List<SupportTicketDto>> GetTicketsByCustomerId(int customerId)
    {
      return await db.SupportTickets
          .Where(t => t.CustomerId == customerId)
          .Select(t => new SupportTicketDto
          {
            TicketId = t.TicketId,
            CustomerId = t.CustomerId,
            CategoryId = t.CategoryId,
            Subject = t.Subject,
            Description = t.Description,
            Status = t.Status,
            Priority = t.Priority,
            CreatedAt = t.CreatedAt,
            UpdatedAt = t.UpdatedAt,
            ClosedAt = t.ClosedAt
          })
          .ToListAsync();
    }

    public async Task<SupportTicketDto> CreateTicket(SupportTicketDto ticket)
    {
      var newTicket = new SupportTicket
      {
        CustomerId = ticket.CustomerId,
        CategoryId = ticket.CategoryId,
        Subject = ticket.Subject,
        Description = ticket.Description,
        Status = "Open",
        Priority = ticket.Priority,
        CreatedAt = DateTime.Now
      };

      db.SupportTickets.Add(newTicket);
      await db.SaveChangesAsync();

      ticket.TicketId = newTicket.TicketId;
      ticket.Status = newTicket.Status;
      ticket.CreatedAt = newTicket.CreatedAt;

      return ticket;
    }

    public async Task<SupportTicketDto?> UpdateTicket(int id, SupportTicketDto ticket)
    {
      var existingTicket = await db.SupportTickets.FindAsync(id);

      if (existingTicket == null)
        return null;

      existingTicket.CustomerId = ticket.CustomerId;
      existingTicket.CategoryId = ticket.CategoryId;
      existingTicket.Subject = ticket.Subject;
      existingTicket.Description = ticket.Description;
      existingTicket.Status = ticket.Status;
      existingTicket.Priority = ticket.Priority;
      existingTicket.UpdatedAt = DateTime.Now;

      if (ticket.Status == "Closed")
        existingTicket.ClosedAt = DateTime.Now;

      await db.SaveChangesAsync();

      return await GetTicketById(id);
    }

    public async Task<bool> DeleteTicket(int id)
    {
      var ticket = await db.SupportTickets.FindAsync(id);

      if (ticket == null)
        return false;

      db.SupportTickets.Remove(ticket);
      await db.SaveChangesAsync();

      return true;
    }
  }
}
