using AutoMapper;
using CustomerRegistration.Application.DTO;
using CustomerRegistration.Application.Interface;
using CustomerRegistration.Domain.Entities;
using CustomerRegistration.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using OtpNet;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CustomerRegistration.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        AppDbContext db;
        IMapper mapper;
      
        public AuthService(AppDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        
        }

        public async Task Register(RegisterUserDto dto)
        {
            if (string.IsNullOrEmpty(dto.Email) || string.IsNullOrEmpty(dto.Password) || string.IsNullOrEmpty(dto.ConfirmPassword))
            {
                throw new Exception("Email, Password and Confirm Password are required");
            }

            if (dto.Password != dto.ConfirmPassword)
            {
                throw new Exception("Passwords do not match");
            }

            string Email = dto.Email.ToLower();
            var existingUser = await db.Users.FirstOrDefaultAsync(u => u.Email == Email);
            if (existingUser != null)
            {
                throw new Exception("User with this email already exists");
            }

            var user = mapper.Map<User>(dto);
            user.Email = dto.Email.ToLower();
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            user.Role = "Customer";
            user.IsActive = true;
            user.TwoFactorEnabled = false;

            await db.Users.AddAsync(user);
            await db.SaveChangesAsync();
        }

        public async Task<User?> Login(LoginDto dto)
        {
            if(string.IsNullOrEmpty(dto.Email) || string.IsNullOrEmpty(dto.Password))
            {
                return null;
            }

            string Email = dto.Email.ToLower();
            var user = await db.Users.FirstOrDefaultAsync(u => u.Email == Email);

            if (user == null)
            {
                return null;
            }

            bool passwordMatch = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);
            if (!passwordMatch)
            {
                return null;
            }
            return user;

        }

        public async Task<User?> GetUserById(int userId)
        {
            return await db.Users.FindAsync(userId);
        }

        
       
    }
}
