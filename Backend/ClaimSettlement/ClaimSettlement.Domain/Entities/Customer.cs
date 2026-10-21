namespace ClaimSettlement.Domain.Entities;

public class Customer
{
    public long CustomerId { get; set; }
    public string FirstName { get; set; } = "";
    public string? MiddleName { get; set; }
    public string LastName { get; set; } = "";
    public DateTime DateOfBirth { get; set; }
    public string Gender { get; set; } = "";
    public string MaritalStatus { get; set; } = "";
    public string MobileNumber { get; set; } = "";
    public string EmailAddress { get; set; } = "";
    public string Occupation { get; set; } = "";
    public decimal AnnualIncome { get; set; }
    public string CustomerStatus { get; set; } = "Active";
}