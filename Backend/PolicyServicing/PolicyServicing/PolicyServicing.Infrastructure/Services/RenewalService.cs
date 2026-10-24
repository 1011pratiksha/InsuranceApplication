using AutoMapper;
using PolicyServicing.Domain.Entities;
using PolicyServicing.Infrastructure.Data;
using PolicySevicing.Application.DTO;
using PolicySevicing.Application.Interface;
using PolicySevicing.Application.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolicyServicing.Infrastructure.Services
{
    public class RenewalService : IRenewalService
    {
        AppDbContext db;
        IMapper mapper;

        PolicyIssuanceClient client;
        public RenewalService(AppDbContext db, IMapper mapper, PolicyIssuanceClient client)
        {
            this.db=db;
            this.mapper = mapper;
            this.client = client;
        }

        public async Task<RenewalDto2> RenewalbyId(long id)
        {
            var ren = await db.PolicyRenewals.FindAsync(id);
             var renew = mapper.Map<RenewalDto2>(ren);
            return renew;
        }

        public async Task<string> RenewPolicy(long pid, RenewPolicyDto dto)
        {
            var poli = await client.FindPolicyById(pid);
            if (poli == null)
            { 
                return "Policy not found";
            }
            if (dto.premium_amount <= 0)
            {
                return "Premium amount must be greater than zero";
            }
            if (dto.new_expiry_date.Date <= DateTime.UtcNow.Date)
            {
                return "New expiry date must be in the future";
            }

            var rep= mapper.Map<PolicyRenewal>(dto);
            rep.policy_id = pid;
            rep.renewal_date = DateTime.Now;
            rep.renewal_status = "Pending";
            db.PolicyRenewals.Add(rep);
            await db.SaveChangesAsync();
            return "Policy renewal request created successfully";
        }
    }
}
