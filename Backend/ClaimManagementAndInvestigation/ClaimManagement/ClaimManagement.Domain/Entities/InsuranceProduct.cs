using System.ComponentModel.DataAnnotations;

namespace ClaimManagement.Domain.Entities
{
    public class InsuranceProduct
    {
        [Key]
        public long ProductId { get; set; }

        [Required]
        [MaxLength(30)]
        public string ProductCode { get; set; } = string.Empty;

        [Required]
        [MaxLength(150)]
        public string ProductName { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string ProductType { get; set; } = string.Empty;

        public string? Description { get; set; }

        public int? MinimumAge { get; set; }

        public int? MaximumAge { get; set; }

        public decimal? MinimumSumInsured { get; set; }

        public decimal? MaximumSumInsured { get; set; }

        [MaxLength(20)]
        public string Status { get; set; } = string.Empty;
    }
}

