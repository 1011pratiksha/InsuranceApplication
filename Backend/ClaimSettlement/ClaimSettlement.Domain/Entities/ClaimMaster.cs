namespace ClaimSettlement.Domain.Entities;

public class ClaimMaster
{
    public long ClaimId { get; set; }
    public string ClaimNumber { get; set; } = "";
    public long PolicyId { get; set; }
    public long CustomerId { get; set; }
    public string ClaimType { get; set; } = "";
    public DateTime IncidentDate { get; set; }
    public DateTime ClaimDate { get; set; }
    public decimal EstimatedLoss { get; set; }
    public string ClaimStatus { get; set; } = "Registered";
}