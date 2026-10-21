using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PolicyIssuance.Domain.Entities;

public class InsuranceProduct
{
    [Key]
    public int ProductId { get; set; }

    [Required]
    public int CategoryId { get; set; }

    [Required]
    public string ProductCode { get; set; } = string.Empty;

    [Required]
    public string ProductName { get; set; } = string.Empty;

    [Required]
    public string ProductType { get; set; } = string.Empty;

    public string? Description { get; set; }

    [Required]
    public int MinimumAge { get; set; }

    [Required]
    public int MaximumAge { get; set; }

    [Required]
    public decimal MinimumSumInsured { get; set; }

    [Required]
    public decimal MaximumSumInsured { get; set; }

    [Required]
    public string Status { get; set; } = string.Empty;
}
