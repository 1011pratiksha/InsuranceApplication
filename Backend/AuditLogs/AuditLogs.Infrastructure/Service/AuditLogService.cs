
using AuditLogs.Application.DTO;
using AuditLogs.Application.Interfaces;
using AuditLogs.Domain.Entities;
using AuditLogs.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AuditLogs.Infrastructure.Services
{
  public class AuditLogService : IAuditLogService
  {
    private readonly AppDbContext db;
    public AuditLogService(AppDbContext db)
    {
      this.db = db;
    }

    // 1. Get All Audit Logs
    public async Task<List<AuditLogDto>> GetAllAsync()
    {
      var auditLogs = await db.AuditLogs.ToListAsync();
      var result = auditLogs.Select(e => new AuditLogDto
      {
        AuditLogId = e.AuditLogId,
        CustomerId = e.CustomerId,
        Action = e.Action,
        EntityName = e.EntityName,
        EntityId = e.EntityId,
        Description = e.Description,
        CreatedAt = e.CreatedAt
      }).ToList();

      return result;
    }

    // 2. Get Audit Log By ID
    public async Task<AuditLogDto?> GetByIdAsync(int id)
    {
      var e = await db.AuditLogs.FindAsync(id);
      if (e == null)
      {
        return null;
      }

      var result = new AuditLogDto
      {
        AuditLogId = e.AuditLogId,
        CustomerId = e.CustomerId,
        Action = e.Action,
        EntityName = e.EntityName,
        EntityId = e.EntityId,
        Description = e.Description,
        CreatedAt = e.CreatedAt
      };

      return result;
    }

    // 3. Get Audit Logs By Customer ID
    public async Task<List<AuditLogDto>> GetByCustomerIdAsync(int customerId)
    {
      var auditLogs = await db.AuditLogs.Where(e => e.CustomerId == customerId)
          .ToListAsync();
      var result = auditLogs.Select(e => new AuditLogDto
      {
        AuditLogId = e.AuditLogId,
        CustomerId = e.CustomerId,
        Action = e.Action,
        EntityName = e.EntityName,
        EntityId = e.EntityId,
        Description = e.Description,
        CreatedAt = e.CreatedAt
      }).ToList();

      return result;
    }

    // 4. Add New Audit Log
    public async Task<AuditLogDto> AddAsync(AuditLogDto auditLogDto)
    {
      var auditLog = new AuditLog
      {
        CustomerId = auditLogDto.CustomerId,
        Action = auditLogDto.Action,
        EntityName = auditLogDto.EntityName,
        EntityId = auditLogDto.EntityId,
        Description = auditLogDto.Description,
        CreatedAt = DateTime.Now
      };

      db.AuditLogs.Add(auditLog);
      await db.SaveChangesAsync();

      auditLogDto.AuditLogId = auditLog.AuditLogId;
      auditLogDto.CreatedAt = auditLog.CreatedAt;

      return auditLogDto;
    }

    // 5. Update Existing Audit Log
    public async Task<bool> UpdateAsync(int id, AuditLogDto auditLogDto)
    {
      var auditLog = await db.AuditLogs.FindAsync(id);

      if (auditLog == null)
      {
        return false;
      }

      auditLog.CustomerId = auditLogDto.CustomerId;
      auditLog.Action = auditLogDto.Action;
      auditLog.EntityName = auditLogDto.EntityName;
      auditLog.EntityId = auditLogDto.EntityId;
      auditLog.Description = auditLogDto.Description;

      await db.SaveChangesAsync();

      return true;
    }

    // 6. Delete Audit Log
    public async Task<bool> DeleteAsync(int id)
    {
      var auditLog = await db.AuditLogs.FindAsync(id);

      if (auditLog == null)
      {
        return false;
      }
      db.AuditLogs.Remove(auditLog);
      await db.SaveChangesAsync();

      return true;
    }



    public async Task LogActionAsync(
          int customerId,
          string action,
          string entityName,
          int entityId,
          string? description)
    {
      var auditLog = new AuditLog
      {
        CustomerId = customerId,
        Action = action,
        EntityName = entityName,
        EntityId = entityId,
        Description = description,
        CreatedAt = DateTime.Now
      };

      db.AuditLogs.Add(auditLog);

      await db.SaveChangesAsync();
    }

  }
}


//❌ Customer ya Policy ki Create, Update, Delete API chalne par audit log apne aap save ho.

//❌ Un APIs ke saath automatic logging ka integration.
