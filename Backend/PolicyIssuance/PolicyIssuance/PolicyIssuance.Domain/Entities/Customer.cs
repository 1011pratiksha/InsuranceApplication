using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PolicyIssuance.Domain.Entities;

public class Customer
{
    [Key]
    public int CustomerId { get; set; }

    [Required]
    public string CustomerCode { get; set; } = string.Empty;

    [Required]
    public string FirstName { get; set; } = string.Empty;

    public string? MiddleName { get; set; }

    [Required]
    public string LastName { get; set; } = string.Empty;

    [Required]
    public DateTime DateOfBirth { get; set; }

    [Required]
    public string Gender { get; set; } = string.Empty;

    [Required]
    public string MaritalStatus { get; set; } = string.Empty;

    [Required]
    public string Occupation { get; set; } = string.Empty;

    [Required]
    public decimal AnnualIncome { get; set; }

    [Required]
    public string MobileNumber { get; set; } = string.Empty;

    [Required]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string CustomerStatus { get; set; } = string.Empty;
}
