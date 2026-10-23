using System.ComponentModel.DataAnnotations;

namespace PolicyIssuance.Domain.Entities;

public class User
{
    [Key]
    public int UserId { get; set; }

    [Required]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string PasswordHash { get; set; } = string.Empty;

    [Required]
    public string Role { get; set; } = string.Empty;

    public bool IsActive { get; set; }

    public bool TwoFactorEnabled { get; set; }

    public DateTime CreatedAt { get; set; }
}
