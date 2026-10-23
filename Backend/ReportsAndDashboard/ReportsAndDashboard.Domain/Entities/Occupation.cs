
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReportsAndDashboard.Domain.Entities
{
  public class Occupation
  {
    [Key]
    public long OccupationId { get; set; }

    public string OccupationName { get; set; } = string.Empty;

    // Navigation collection

    public List<Customer> Customers { get; set; }
        = new List<Customer>();
  }
}
