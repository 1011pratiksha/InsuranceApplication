using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PolicyIssuance.Domain.Entities;

public class Quote
{
    [Key]
    public int QuoteId { get; set; }

    [Required]
    public string QuoteNumber { get; set; } = string.Empty;

    [Required]
    public int CustomerId { get; set; }

    [Required]
    public int ProductId { get; set; }

    [Required]
    public int PolicyTerm { get; set; }

    [Required]
    public decimal SumInsured { get; set; }

    [Required]
    public int PremiumFrequencyId { get; set; }

    [Required]
    public DateTime QuoteDate { get; set; }

    [Required]
    public DateTime ExpiryDate { get; set; }

    [Required]
    public string QuoteStatus { get; set; } = string.Empty;
}
