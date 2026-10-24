using AutoMapper;
using KycVerification.Application.DTO;
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
    public class CustomerKycServices : ICustomerKycServices
    {

        private readonly AppDbContext db;
        private readonly IMapper mapper;

        public CustomerKycServices( AppDbContext db,IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<CustomerKyc?> GetByIdAsync(int kycId)
        {
            var data = await db.CustomerKycs
                .FirstOrDefaultAsync(x => x.KycId == kycId);

            return data;
        }

        public async Task<CustomerKyc?> GetByCustomerIdAsync(int customerId)
        {
            var data = await db.CustomerKycs
                .FirstOrDefaultAsync(x => x.CustomerId == customerId);

            return data;
        }

        public async Task<List<CustomerKyc>> GetAllAsync()
        {
            var data = await db.CustomerKycs
                //.AsNoTracking()
                .ToListAsync();

            return data;
        }

        public async Task<CustomerKyc> AddAsync(CustomerKyc kyc)
        {
            var map = mapper.Map<CustomerKyc>(kyc);

            await db.CustomerKycs.AddAsync(map);
            await db.SaveChangesAsync();

            return map;
        }

        public async Task UpdateAsync(CustomerKyc kyc)
        {
            var map = mapper.Map<CustomerKyc>(kyc);

            db.CustomerKycs.Update(map);
            await db.SaveChangesAsync();
        }
    }



}



