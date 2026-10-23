using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PolicyIssuance.Domain.Entities;

public class PolicyRider
{
    [Key]
    public int PolicyRiderId { get; set; }

    [Required]
    public int PolicyId { get; set; }

    [Required]
    public int RiderId { get; set; }

    [Required]
    public decimal RiderPremium { get; set; }

    public Policy? Policy { get; set; }
}
