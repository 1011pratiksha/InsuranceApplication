using System.ComponentModel.DataAnnotations;

namespace InsuranceAgent.Domain.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        public int EmployeeId { get; set; }

        [Required]
        [StringLength(100)]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string PasswordHash { get; set; } = string.Empty;

        [StringLength(150)]
        public string? Email { get; set; }

        [StringLength(20)]
        public string? Mobile { get; set; }

        public int RoleId { get; set; }

        public int BranchId { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; } = "Active";
    }
}