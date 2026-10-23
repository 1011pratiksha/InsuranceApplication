using AutoMapper;
using CustomerRegistration.Application.DTO;
using CustomerRegistration.Application.Interface;
using CustomerRegistration.Domain.Entities;
using CustomerRegistration.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerRegistration.Infrastructure.Services
{
    public class CustomerService : ICustomerService
    {
        AppDbContext db;
        IMapper mapper;

        public CustomerService(AppDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<string> AddCustomer(CreateCustomerDto dto)
        {
            var customer = mapper.Map<Customer>(dto);
            customer.Email = dto.Email.ToLower();

            if (await db.Customers.AnyAsync(x =>x.Email == customer.Email || x.MobileNumber == customer.MobileNumber))
            {
                return "Email or mobile already exists";
            }

            customer.CustomerCode = "C" + Guid.NewGuid().ToString("N").Substring(0, 19);
            customer.CustomerStatus = "Active";

            db.Customers.Add(customer);
            await db.SaveChangesAsync();
            return "Customer added successfully";
        }

        public async Task<List<CustomerResponseDto>> FetchCustomers()
        {
            var data = await db.Customers.ToListAsync();
            return mapper.Map<List<CustomerResponseDto>>(data);
        }

        public async Task<CustomerResponseDto?> FindCustomerById(int id)
        {
            var customer = await db.Customers.FindAsync(id);
            if (customer == null)
            {
                return null;
            }
            var data = mapper.Map<CustomerResponseDto>(customer);
            return data;
        }

        public async Task<string> UpdateCustomer(int id, UpdateCustomerDto dto)
        {
            var customer = await db.Customers.FindAsync(id);
            if (customer == null)
            {
                return "Customer not found";
            }
               
            string email = dto.Email.ToLower();
            if (await db.Customers.AnyAsync(x =>x.CustomerId != id && (x.Email == email || x.MobileNumber == dto.MobileNumber)))
            {
                return "Email or mobile already exists";
            }

            mapper.Map(dto, customer);
            customer.Email = email;
            customer.UpdatedAt = DateTime.UtcNow;

            await db.SaveChangesAsync();
            return "Customer updated successfully";
        }

        public async Task<string> DeleteCustomer(int id)
        {
            var customer = await db.Customers.FindAsync(id);
            if (customer == null)
            {
                return "Customer not found";
            }

            db.Customers.Remove(customer);
            await db.SaveChangesAsync();
            return "Customer deleted successfully";
        }

        public async Task<List<CustomerResponseDto>> SearchCustomers(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                return new List<CustomerResponseDto>();
            }

            var data = await db.Customers.Where(x =>
            x.FirstName.Contains(searchText) || x.LastName.Contains(searchText)).ToListAsync();

            return mapper.Map<List<CustomerResponseDto>>(data);
        }
    }
}