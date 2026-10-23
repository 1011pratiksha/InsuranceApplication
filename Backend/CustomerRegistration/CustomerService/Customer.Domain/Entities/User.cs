using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CustomerRegistration.Domain.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public string Role { get; set; }

        public bool IsActive { get; set; }

        public bool TwoFactorEnabled { get; set; }

        public string? TwoFactorSecretKey { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
