using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PolicyIssuance.Domain.Entities;

public class PolicyHistory
{
    [Key]
    public int HistoryId { get; set; }

    [Required]
    public int PolicyId { get; set; }

    [Required]
    public string Action { get; set; } = string.Empty;

    public int? PerformedBy { get; set; }

    [Required]
    public DateTime ActionDate { get; set; }

    public string? Remarks { get; set; }

    public Policy? Policy { get; set; }
}
