namespace ClaimSettlement.Domain.Entities;

public class Policy
{
    public long PolicyId { get; set; }
    public string PolicyNumber { get; set; } = "";
    public long CustomerId { get; set; }
    public string PolicyStatus { get; set; } = "";
    public decimal SumInsured { get; set; }
    public DateTime PolicyExpiryDate { get; set; }
}