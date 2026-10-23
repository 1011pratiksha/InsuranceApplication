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
            ens.approval_status = "Approved";
            await db.SaveChangesAsync();
        }

        public async Task createEndorsement(EndorsementDto dto)
        {
            var reqe = db.ServiceRequests.FirstOrDefault(x => x.request_id == dto.request_id);
            var es= mapper.Map<PolicyEndorsement>(dto);
            es.endorsement_number = "END-" + Guid.NewGuid().ToString("N")[..26].ToUpperInvariant();
            es.approval_status = "Pending";
            db.PolicyEndorsements.Add(es);
            await db.SaveChangesAsync();
            //return mapper.Map<EndorsementDto2>(es);

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
            ens.approval_status = "Rejected";
            await db.SaveChangesAsync();
        }
    }
}
