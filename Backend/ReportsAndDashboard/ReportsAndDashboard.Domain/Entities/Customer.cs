
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReportsAndDashboard.Domain.Entities
{
  public class Customer
  {
    [Key]
    public long CustomerId { get; set; }

    public string CustomerCode { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string? MiddleName { get; set; }

    public string LastName { get; set; } = string.Empty;

    public DateTime DateOfBirth { get; set; }

    public long GenderId { get; set; }

    public long MaritalStatusId { get; set; }

    public long OccupationId { get; set; }

    public string MobileNumber { get; set; } = string.Empty;

    public string? AlternateMobile { get; set; }

    public string Email { get; set; } = string.Empty;

    public string? OfficeNumber { get; set; }

    public decimal AnnualIncome { get; set; }

    public string CustomerStatus { get; set; } = "Active";

    public DateTime CreatedAt { get; set; }

    // Foreign key relationships

    [ForeignKey(nameof(GenderId))]
    public Gender Gender { get; set; } = null!;

    [ForeignKey(nameof(MaritalStatusId))]
    public MaritalStatus MaritalStatus { get; set; } = null!;

    [ForeignKey(nameof(OccupationId))]
    public Occupation Occupation { get; set; } = null!;

    // Navigation collections

    public List<CustomerAddress> Addresses { get; set; }
        = new List<CustomerAddress>();

    public List<CustomerKyc> KycRecords { get; set; }
        = new List<CustomerKyc>();

    public List<CustomerNominee> Nominees { get; set; }
        = new List<CustomerNominee>();

    public List<CustomerDocument> Documents { get; set; }
        = new List<CustomerDocument>();

    public List<Policy> Policies { get; set; }
        = new List<Policy>();
  }
}
