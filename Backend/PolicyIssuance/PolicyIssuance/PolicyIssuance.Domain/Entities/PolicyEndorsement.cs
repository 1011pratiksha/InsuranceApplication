using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PolicyIssuance.Domain.Entities;

public class PolicyEndorsement
{
    [Key]
    public int EndorsementId { get; set; }

    [Required]
    public int PolicyId { get; set; }

    [Required]
    public string EndorsementType { get; set; } = string.Empty;

    public string? OldValue { get; set; }

    public string? NewValue { get; set; }

    public int? ApprovedBy { get; set; }

    [Required]
    public DateTime EndorsementDate { get; set; }

    public Policy? Policy { get; set; }
}
