using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PolicyServicing.Domain.Entities;
using PolicyServicing.Infrastructure.Data;
using PolicySevicing.Application.DTO;
using PolicySevicing.Application.Interface;
using PolicySevicing.Application.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PolicyServicing.Infrastructure.Services
{
    public class CancellationService : ICancellationService
    {
        AppDbContext db;
        IMapper mapper;
        PolicyIssuanceClient client;
        public CancellationService(AppDbContext db, IMapper mapper,PolicyIssuanceClient client)
        {
            this.db = db;
            this.mapper = mapper;
            this.client = client;
        }
        public async Task<CancellationDto?> CancellationById(long id)
        {
            var cancel = await db.PolicyCancellations.FirstOrDefaultAsync(x => x.cancellation_id == id);
            var dto = mapper.Map<CancellationDto>(cancel);
            return dto;
        }

        public async Task<string> CancelPolicy(long id, CancelPolicyDto dto)
        {
            var policy = await client.FindPolicyById(id);
            if (policy == null)
            {
                return "Policy not found";
            }
            var excancel = await db.PolicyCancellations.FirstOrDefaultAsync(x => x.policy_id == id);
            var cancellation = mapper.Map<PolicyCancellation>(dto);
            cancellation.policy_id = id;
            cancellation.cancellation_date = DateTime.Now;
            cancellation.refund_amount = 0;
            cancellation.approved_by = 1;
            db.PolicyCancellations.Add(cancellation); ;
            await db.SaveChangesAsync();
            return "Policy cancellation request created successfully";
        }
    }
}
