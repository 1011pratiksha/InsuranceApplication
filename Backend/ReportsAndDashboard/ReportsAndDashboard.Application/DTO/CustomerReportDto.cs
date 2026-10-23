
using System;

namespace ReportsAndDashboard.Application.DTO
{
  public class CustomerReportDto
  {
    public long CustomerId { get; set; }

    public string CustomerCode { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string? MiddleName { get; set; }

    public string LastName { get; set; } = string.Empty;

    public DateTime DateOfBirth { get; set; }

    public string GenderName { get; set; } = string.Empty;

    public string MaritalStatusName { get; set; } = string.Empty;

    public string OccupationName { get; set; } = string.Empty;

    public string MobileNumber { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public decimal AnnualIncome { get; set; }

    public string CustomerStatus { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public string? CityName { get; set; }

    public string? StateName { get; set; }

    public string? KycStatus { get; set; }
    public long? ProductId { get; set; }

    public string? ProductName { get; set; }
  }
}
