
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReportsAndDashboard.Domain.Entities
{
  public class MaritalStatus
  {
    [Key]
    public long MaritalStatusId { get; set; }

    public string MaritalStatusName { get; set; } = string.Empty;

    // Navigation collection

    public List<Customer> Customers { get; set; }
        = new List<Customer>();
  }
}
