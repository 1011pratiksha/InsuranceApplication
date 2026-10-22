using AutoMapper;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
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
    public class ServiceRequestService : IServiceRequestService
    {
        AppDbContext db;
        IMapper mapper;
        PolicyIssuanceClient client;
        public ServiceRequestService(AppDbContext db, IMapper mapper, PolicyIssuanceClient client)
        {
            this.db = db;
            this.mapper = mapper;
            this.client = client;
            
        }
        public async Task CreateServiceRequest(CreateServiceRequestDto dto)
        {
            var request =  mapper.Map<ServiceRequest>(dto);
            request.request_number = "SR-" + Guid.NewGuid().ToString("N")[..27].ToUpperInvariant();
            request.request_date = DateTime.Now;
            request.status = "Submitted";
            db.ServiceRequests.Add(request);
            await db.SaveChangesAsync();

        }

        public async Task<List<ServiceRequestResponseDto>> GetAllServiceRequests()
        {
            var reqs = await db.ServiceRequests.ToListAsync();
           var res= mapper.Map<List<ServiceRequestResponseDto>>(reqs);
            return res;
        }

        public async Task<ServiceRequestResponseDto?> GetServiceRequestById(long id)
        {
            var req = await db.ServiceRequests.FindAsync(id);
            if (req == null)
                return null;
            var re= mapper.Map<ServiceRequestResponseDto>(req);
            return re;
        }

        public async Task UpdateServiceRequest(long id, UpdateServiceRequestDto dto)
        {
            var req = db.ServiceRequests.FirstOrDefault(x=>x.request_id == id);
            req.request_type = dto.request_type;
            await db.SaveChangesAsync();
        }
    }
}
