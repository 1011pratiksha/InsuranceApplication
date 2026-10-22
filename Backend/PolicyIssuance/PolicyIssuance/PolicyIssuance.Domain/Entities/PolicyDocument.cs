using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PolicyIssuance.Domain.Entities;

public class PolicyDocument
{
    [Key]
    public int DocumentId { get; set; }

    [Required]
    public int PolicyId { get; set; }

    [Required]
    public string DocumentName { get; set; } = string.Empty;

    [Required]
    public string FilePath { get; set; } = string.Empty;

    [Required]
    public int VersionNo { get; set; }

    [Required]
    public DateTime GeneratedDate { get; set; }

    public Policy? Policy { get; set; }
}
