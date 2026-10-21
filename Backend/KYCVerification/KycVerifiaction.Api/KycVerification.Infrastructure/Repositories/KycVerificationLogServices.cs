using AutoMapper;
using KycVerification.Application.Interface;
using KycVerification.Domain.Entities;
using KycVerification.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KycVerification.Infrastructure.Repositories
{
    public class KycVerificationLogServices : IKycVerificationLogServices
    {
        private readonly AppDbContext db;
        private readonly IMapper mapper;

        public KycVerificationLogServices( AppDbContext db,IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        // Get audit log by ID
        public async Task<KycVerificationLog?> GetByIdAsync(int logId)
        {
            return await db.KycVerificationLogs
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.LogId == logId);
        }

        // Get audit history for a specific KYC record
        public async Task<List<KycVerificationLog>> GetByKycIdAsync(
            int kycId)
        {
            return await db.KycVerificationLogs
                .AsNoTracking()
                .Where(x => x.KycId == kycId)
                .OrderByDescending(x => x.ActionDate)
                .ToListAsync();
        }

        // Get all audit logs
        public async Task<List<KycVerificationLog>> GetAllAsync()
        {
            return await db.KycVerificationLogs
                .AsNoTracking()
                .OrderByDescending(x => x.ActionDate)
                .ToListAsync();
        }

        // Add an audit log entry
        public async Task<KycVerificationLog> AddAsync(
            KycVerificationLog log)
        {
            var map = mapper.Map<KycVerificationLog>(log);

            map.LogId = 0;
            map.ActionDate = DateTime.UtcNow;

            await db.KycVerificationLogs.AddAsync(map);
            await db.SaveChangesAsync();

            return map;
        }
    }
}
