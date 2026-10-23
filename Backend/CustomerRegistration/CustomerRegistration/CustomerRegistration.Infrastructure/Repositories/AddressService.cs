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
    public class AddressService : IAddressService
    {
        AppDbContext db;
        IMapper mapper;

        public AddressService(AppDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<string> AddAddress(int customerId, CreateAddressDto dto)
        {
            var customer = await db.Customers.FindAsync(customerId);
            if (customer == null)
            {
                return "Customer not found";
            }

            var address = mapper.Map<CustomerAddress>(dto);
            address.CustomerId = customerId;

            db.CustomerAddresses.Add(address);
            await db.SaveChangesAsync();
            return "Address added successfully";
        }

        public async Task<List<CustomerAddressDto>> FetchAddressesByCustomerId(int customerId)
        {
            var data = await db.CustomerAddresses
                .Where(x => x.CustomerId == customerId)
                .ToListAsync();

            return mapper.Map<List<CustomerAddressDto>>(data);
        }

        public async Task<CustomerAddressDto?> FindAddressById(int id)
        {
            var address = await db.CustomerAddresses.FindAsync(id);
            if (address == null)
            {
                return null;
            }
            return mapper.Map<CustomerAddressDto>(address);
        }

        public async Task<string> UpdateAddress(int id, CreateAddressDto dto)
        {
            var address = await db.CustomerAddresses.FindAsync(id);
            if (address == null)
            {
                return "Address not found";
            }

            mapper.Map(dto, address);
            await db.SaveChangesAsync();
            return "Address updated successfully";
        }

        public async Task<string> DeleteAddress(int id)
        {
            var address = await db.CustomerAddresses.FindAsync(id);
            if (address == null)
            {
                return "Address not found";
            }

            db.CustomerAddresses.Remove(address);
            await db.SaveChangesAsync();
            return "Address deleted successfully";
        }
    }
}
