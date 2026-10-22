using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PolicyIssuance.Domain.Entities;

public class RiderMaster
{
    [Key]
    public int RiderId { get; set; }

    [Required]
    public int ProductId { get; set; }

    [Required]
    public string RiderName { get; set; } = string.Empty;

    public string? RiderDescription { get; set; }

    [Required]
    public decimal RiderPremium { get; set; }

    [Required]
    public string Status { get; set; } = string.Empty;
}
