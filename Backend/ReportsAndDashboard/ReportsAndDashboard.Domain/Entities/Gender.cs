
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReportsAndDashboard.Domain.Entities
{
  public class Gender
  {
    [Key]
    public long GenderId { get; set; }

    public string GenderName { get; set; } = string.Empty;

    // Navigation collection

    public List<Customer> Customers { get; set; }
        = new List<Customer>();
  }
}
