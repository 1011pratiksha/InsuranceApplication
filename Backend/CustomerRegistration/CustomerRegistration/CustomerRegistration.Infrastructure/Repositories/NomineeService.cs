using AutoMapper;
using Microsoft.EntityFrameworkCore;
using CustomerRegistration.Application.DTO;
using CustomerRegistration.Application.Interfaces;
using CustomerRegistration.Domain.Entities;
using CustomerRegistration.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerRegistration.Infrastructure.Repositories
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

        public async Task<string> AddNominee(int customerId, CreateNomineeDto dto)
        {
            var customer = await db.Customers.FindAsync(customerId);
            if (customer == null)
            {
                return "Customer not found";
            }

            var currentTotal = await db.Nominees
                .Where(x => x.CustomerId == customerId)
                .SumAsync(x => x.SharePercentage);

            if (currentTotal + dto.SharePercentage > 100)
            {
                return $"Total nominee share exceeds 100%. Current total is {currentTotal}%";
            }

            var nominee = mapper.Map<Nominee>(dto);
            nominee.CustomerId = customerId;

            db.Nominees.Add(nominee);
            await db.SaveChangesAsync();
            return "Nominee added successfully";
        }

        public async Task<List<NomineeResponseDto>> FetchNomineesByCustomerId(int customerId)
        {
            var data = await db.Nominees
                .Where(x => x.CustomerId == customerId)
                .ToListAsync();

            return mapper.Map<List<NomineeResponseDto>>(data);
        }

        public async Task<NomineeResponseDto?> FindNomineeById(int id)
        {
            var nominee = await db.Nominees.FindAsync(id);
            if (nominee == null)
            {
                return null;
            }
            return mapper.Map<NomineeResponseDto>(nominee);
        }

        public async Task<string> UpdateNominee(int id, CreateNomineeDto dto)
        {
            var nominee = await db.Nominees.FindAsync(id);
            if (nominee == null)
            {
                return "Nominee not found";
            }

            var otherTotal = await db.Nominees
                .Where(x => x.CustomerId == nominee.CustomerId && x.NomineeId != id)
                .SumAsync(x => x.SharePercentage);

            if (otherTotal + dto.SharePercentage > 100)
            {
                return $"Total nominee share exceeds 100%. Other nominees total is {otherTotal}%";
            }

            mapper.Map(dto, nominee);
            await db.SaveChangesAsync();
            return "Nominee updated successfully";
        }

        public async Task<string> DeleteNominee(int id)
        {
            var nominee = await db.Nominees.FindAsync(id);
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
