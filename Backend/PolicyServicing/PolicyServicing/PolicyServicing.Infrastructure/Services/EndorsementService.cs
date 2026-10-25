using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PolicyServicing.Domain.Entities;
using PolicyServicing.Infrastructure.Data;
using PolicySevicing.Application.DTO;
using PolicySevicing.Application.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolicyServicing.Infrastructure.Services
{
    public class EndorsementService : IEndorsementService
    {
        AppDbContext db;
        IMapper mapper;
        public EndorsementService(AppDbContext db,IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }
        public async Task ApprovedEnd(long id)
        {
            var ens = db.PolicyEndorsements.Find(id);
            ens.approvel_status = "Approved";
            await db.SaveChangesAsync();
        }

        public async Task createEndorsement(EndorsementDto dto)
        {
            var request = await db.ServiceRequests
                .FirstOrDefaultAsync(x => x.customer_id == dto.request_id);

            if (request == null)
                throw new KeyNotFoundException("Service request not found.");

            var endorsement = new PolicyEndorsement
            {
                policy_id = checked((int)request.policy_id),
                endorsement_type = dto.endorsement_type,
                previous_value = dto.previous_value,
                updated_value = dto.updated_value,
                endorsement_date = DateTime.UtcNow
            };

            db.PolicyEndorsements.Add(endorsement);
            await db.SaveChangesAsync();
        }

        public async Task<EndorsementDto2> fetchendorse(long id)
        {
            var en = await db.PolicyEndorsements.FindAsync(id);
           var ens= mapper.Map<EndorsementDto2>(en);
            return ens;
        }

        public async Task RejectEnd(long id)
        {
           var ens = db.PolicyEndorsements.Find(id);
            ens.approvel_status = "Rejected";
            await db.SaveChangesAsync();
        }
    }
}
