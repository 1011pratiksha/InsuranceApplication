using AutoMapper;
using CustomerRegistration.Application.DTO;
using CustomerRegistration.Application.Interface;
using CustomerRegistration.Domain.Entities;
using CustomerRegistration.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerRegistration.Infrastructure.Services
{
    public class NomineeService : INomineeService
    {
        AppDbContext db;
        IMapper mapper;
        public NomineeService(AppDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<string> AddNominee(int customerId, NomineeDto dto)
        {
            var customer = await db.Customers.FindAsync(customerId);
            if (customer == null)
            {
                return "Customer not found";
            }

            var nominee = mapper.Map<Nominee>(dto);
            nominee.CustomerId = customerId;
            db.Nominees.Add(nominee);
            await db.SaveChangesAsync();
            return "Nominee added successfully";
        }

        public async Task<List<NomineeDto>> FetchNominees(int customerId)
        {
            var data = await db.Nominees.Where(x => x.CustomerId == customerId).ToListAsync();
            return mapper.Map<List<NomineeDto>>(data);
        }

        public async Task<string> UpdateNominee(int nomineeId, NomineeDto dto)
        {
            var nominee = await db.Nominees.FindAsync(nomineeId);
            if (nominee == null)
            {
                return "Nominee not found";
            }
            mapper.Map(dto, nominee);
            await db.SaveChangesAsync();
            return "Nominee updated successfully";
        }

        public async Task<string> DeleteNominee(int nomineeId)
        {
            var nominee = await db.Nominees.FindAsync(nomineeId);
            if (nominee == null)
            {
                return "Nominee not found";
            }
            db.Nominees.Remove(nominee);
            await db.SaveChangesAsync();
            return "Nominee deleted successfully";
        }
    }
}