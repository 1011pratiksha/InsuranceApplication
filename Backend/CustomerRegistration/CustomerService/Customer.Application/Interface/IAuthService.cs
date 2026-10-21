using CustomerRegistration.Application.DTO;
using CustomerRegistration.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerRegistration.Application.Interface
{
    public interface IAuthService
    {
        Task Register(RegisterUserDto dto);
        Task<User?> Login(LoginDto dto);
        Task<User?> GetUserById(int userId);
    }
}
