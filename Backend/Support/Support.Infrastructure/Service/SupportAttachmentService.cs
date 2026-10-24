
using Microsoft.EntityFrameworkCore;
using Support.Application.DTOs;
using Support.Application.Interfaces;
using Support.Domain.Entities;
using Support.Infrastructure.Data;

namespace Support.Infrastructure.Services
{
  public class SupportAttachmentService : ISupportAttachmentService
  {
    private readonly AppDbContext db;

    public SupportAttachmentService(AppDbContext db)
    {
      this.db = db;
    }

    // 1. Get all attachments
    public async Task<List<SupportAttachmentDto>> GetAllAttachments()
    {
      return await db.SupportAttachments
          .Select(a => new SupportAttachmentDto
          {
            AttachmentId = a.AttachmentId,
            TicketId = a.TicketId,
            FileName = a.FileName,
            FilePath = a.FilePath,
            ContentType = a.ContentType,
            UploadedAt = a.UploadedAt
          })
          .ToListAsync();
    }

    // 2. Get attachment by ID
    public async Task<SupportAttachmentDto?> GetAttachmentById(int id)
    {
      var a = await db.SupportAttachments.FindAsync(id);

      if (a == null)
        return null;

      return new SupportAttachmentDto
      {
        AttachmentId = a.AttachmentId,
        TicketId = a.TicketId,
        FileName = a.FileName,
        FilePath = a.FilePath,
        ContentType = a.ContentType,
        UploadedAt = a.UploadedAt
      };
    }

    // 3. Get attachments by ticket ID
    public async Task<List<SupportAttachmentDto>> GetAttachmentsByTicketId(int ticketId)
    {
      return await db.SupportAttachments
          .Where(a => a.TicketId == ticketId)
          .Select(a => new SupportAttachmentDto
          {
            AttachmentId = a.AttachmentId,
            TicketId = a.TicketId,
            FileName = a.FileName,
            FilePath = a.FilePath,
            ContentType = a.ContentType,
            UploadedAt = a.UploadedAt
          })
          .ToListAsync();
    }

    // 4. Create attachment
    public async Task<SupportAttachmentDto> CreateAttachment(SupportAttachmentDto attachment)
    {
      var newAttachment = new SupportAttachment
      {
        TicketId = attachment.TicketId,
        FileName = attachment.FileName,
        FilePath = attachment.FilePath,
        ContentType = attachment.ContentType,
        UploadedAt = DateTime.Now
      };

      db.SupportAttachments.Add(newAttachment);
      await db.SaveChangesAsync();

      attachment.AttachmentId = newAttachment.AttachmentId;
      attachment.UploadedAt = newAttachment.UploadedAt;

      return attachment;
    }

    // 5. Update attachment
    public async Task<SupportAttachmentDto?> UpdateAttachment(int id, SupportAttachmentDto attachment)
    {
      var existingAttachment = await db.SupportAttachments.FindAsync(id);

      if (existingAttachment == null)
        return null;

      existingAttachment.TicketId = attachment.TicketId;
      existingAttachment.FileName = attachment.FileName;
      existingAttachment.FilePath = attachment.FilePath;
      existingAttachment.ContentType = attachment.ContentType;

      await db.SaveChangesAsync();

      return await GetAttachmentById(id);
    }

    // 6. Delete attachment
    public async Task<bool> DeleteAttachment(int id)
    {
      var attachment = await db.SupportAttachments.FindAsync(id);

      if (attachment == null)
        return false;

      db.SupportAttachments.Remove(attachment);
      await db.SaveChangesAsync();

      return true;
    }
  }
}
