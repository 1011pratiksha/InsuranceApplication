
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Support.Domain.Entities
{
  public class Customer
  {
    [Key]
    public int CustomerId { get; set; }

    [Required]
    [StringLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [StringLength(50)]
    public string? MiddleName { get; set; }

    [Required]
    [StringLength(50)]
    public string LastName { get; set; } = string.Empty;

    public DateTime DateOfBirth { get; set; }

    [Required]
    [StringLength(10)]
    public string Gender { get; set; } = string.Empty;

    [StringLength(20)]
    public string? MaritalStatus { get; set; }

    [Required]
    [Phone]
    [StringLength(15)]
    public string MobileNumber { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [StringLength(100)]
    public string EmailAddress { get; set; } = string.Empty;

    [StringLength(100)]
    public string? Occupation { get; set; }

    [Range(0, double.MaxValue)]
    public decimal AnnualIncome { get; set; }

    [Required]
    [StringLength(20)]
    public string CustomerStatus { get; set; } = "Active";
  }
}
