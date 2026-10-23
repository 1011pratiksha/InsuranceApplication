
using Support.Application.DTOs;

namespace Support.Application.Interfaces
{
  public interface ISupportService
  {
    // Get all support tickets
    Task<List<SupportTicketDto>> GetAllTickets();

    // Get ticket by ID
    Task<SupportTicketDto?> GetTicketById(int id);

    // Get tickets by customer ID
    Task<List<SupportTicketDto>> GetTicketsByCustomerId(int customerId);

    // Create ticket
    Task<SupportTicketDto> CreateTicket(SupportTicketDto ticket);

    // Update ticket
    Task<SupportTicketDto?> UpdateTicket(int id, SupportTicketDto ticket);

    // Delete ticket
    Task<bool> DeleteTicket(int id);
  }
}
