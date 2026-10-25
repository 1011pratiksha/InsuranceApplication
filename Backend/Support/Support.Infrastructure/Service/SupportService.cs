
using Microsoft.EntityFrameworkCore;
using Support.Application.DTOs;
using Support.Application.Interfaces;
using Support.Domain.Entities;
using Support.Infrastructure.Data;
using System;

namespace Support.Infrastructure.Services
{
  public class SupportService : ISupportService
  {
    private readonly AppDbContext db;
    private readonly ICustomerApiService customerApiService;
    private readonly IAuditLogApiService auditLogApiService;

    public SupportService(
        AppDbContext db,
        ICustomerApiService customerApiService,
        IAuditLogApiService auditLogApiService)
    {
      this.db = db;
      this.customerApiService = customerApiService;
      this.auditLogApiService = auditLogApiService;
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
      if (id <= 0)
        return null;

      var ticket = await db.SupportTickets.FindAsync(id);

      if (ticket == null)
        return null;

      return new SupportTicketDto
      {
        TicketId = ticket.TicketId,
        CustomerId = ticket.CustomerId,
        CategoryId = ticket.CategoryId,
        Subject = ticket.Subject,
        Description = ticket.Description,
        Status = ticket.Status,
        Priority = ticket.Priority,
        CreatedAt = ticket.CreatedAt,
        UpdatedAt = ticket.UpdatedAt,
        ClosedAt = ticket.ClosedAt
      };
    }

    public async Task<List<SupportTicketDto>> GetTicketsByCustomerId(
        int customerId)
    {
      if (customerId <= 0)
        throw new ArgumentException("Invalid customer ID.");

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

    public async Task<SupportTicketDto> CreateTicket(
        SupportTicketDto ticket)
    {
      if (ticket == null)
        throw new ArgumentException("Ticket data is required.");

      if (ticket.CustomerId <= 0)
        throw new ArgumentException("Invalid customer ID.");

      if (string.IsNullOrWhiteSpace(ticket.Subject))
        throw new ArgumentException("Subject is required.");

      bool customerExists =
          await customerApiService.CustomerExists(ticket.CustomerId);

      if (!customerExists)
        throw new ArgumentException("Customer not found.");

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
      ticket.UpdatedAt = newTicket.UpdatedAt;
      ticket.ClosedAt = newTicket.ClosedAt;

      await auditLogApiService.LogActionAsync(
          newTicket.CustomerId,
          "Create",
          "SupportTicket",
          newTicket.TicketId,
          "Support ticket created successfully.");

      return ticket;
    }

    public async Task<SupportTicketDto?> UpdateTicket(
        int id,
        SupportTicketDto ticket)
    {
      if (id <= 0)
        throw new ArgumentException("Invalid ticket ID.");

      if (ticket == null)
        throw new ArgumentException("Ticket data is required.");

      var existingTicket = await db.SupportTickets.FindAsync(id);

      if (existingTicket == null)
        return null;

      if (ticket.CustomerId <= 0)
        throw new ArgumentException("Invalid customer ID.");

      if (string.IsNullOrWhiteSpace(ticket.Subject))
        throw new ArgumentException("Subject is required.");

      if (existingTicket.CustomerId != ticket.CustomerId)
      {
        bool customerExists =
            await customerApiService.CustomerExists(
                ticket.CustomerId);

        if (!customerExists)
          throw new ArgumentException("Customer not found.");
      }

      existingTicket.CustomerId = ticket.CustomerId;
      existingTicket.CategoryId = ticket.CategoryId;
      existingTicket.Subject = ticket.Subject;
      existingTicket.Description = ticket.Description;
      existingTicket.Priority = ticket.Priority;
      existingTicket.Status = ticket.Status;
      existingTicket.UpdatedAt = DateTime.Now;

      if (string.Equals(
          ticket.Status,
          "Closed",
          StringComparison.OrdinalIgnoreCase))
      {
        existingTicket.ClosedAt ??= DateTime.Now;
        existingTicket.Status = "Closed";
      }
      else
      {
        existingTicket.ClosedAt = null;
      }

      await db.SaveChangesAsync();

      await auditLogApiService.LogActionAsync(
          existingTicket.CustomerId,
          "Update",
          "SupportTicket",
          existingTicket.TicketId,
          "Support ticket updated successfully.");

      return await GetTicketById(id);
    }


    public async Task<bool> DeleteTicket(int id)
    {
      var ticket = await db.SupportTickets.FindAsync(id);

      if (ticket == null)
        return false;

      int customerId = ticket.CustomerId;
      int ticketId = ticket.TicketId;

      db.SupportTickets.Remove(ticket);
      await db.SaveChangesAsync();

      await auditLogApiService.LogActionAsync(
          customerId,
          "Delete",
          "SupportTicket",
          ticketId,
          "Support ticket deleted successfully.");

      return true;
    }

  }
}
