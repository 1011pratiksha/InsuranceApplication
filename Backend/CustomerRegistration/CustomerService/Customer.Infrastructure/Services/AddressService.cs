using AutoMapper;
using CustomerRegistration.Application.DTO;
using CustomerRegistration.Application.Interface;
using CustomerRegistration.Domain.Entities;
using CustomerRegistration.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerRegistration.Infrastructure.Services
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

        public async Task<string> AddAddress(int customerId, CustomerAddressDto dto)
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

        public async Task<List<CustomerAddressDto>> FetchAddresses(int customerId)
        {
            var data = await db.CustomerAddresses.Where(x => x.CustomerId == customerId).ToListAsync();
            var result = mapper.Map<List<CustomerAddressDto>>(data);
            return result;
        }

        public async Task<string> UpdateAddress(int addressId, CustomerAddressDto dto)
        {
            var address = await db.CustomerAddresses.FindAsync(addressId);
            if (address == null)
            {
                return "Address not found";
            }

            mapper.Map(dto, address);
            await db.SaveChangesAsync();
            return "Address updated successfully";
        }

        public async Task<string> DeleteAddress(int addressId)
        {
            var address = await db.CustomerAddresses.FindAsync(addressId);
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
