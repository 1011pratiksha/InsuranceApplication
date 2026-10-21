using System;

namespace CustomerRegistration.Application.DTO
{
    public class UserResponseDto
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}