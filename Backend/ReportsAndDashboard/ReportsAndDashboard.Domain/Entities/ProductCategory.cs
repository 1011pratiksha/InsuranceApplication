
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReportsAndDashboard.Domain.Entities
{
  public class ProductCategory
  {
    [Key]
    public long CategoryId { get; set; }

    public string CategoryName { get; set; } = string.Empty;

    // Navigation collection

    public List<InsuranceProduct> Products { get; set; }
        = new List<InsuranceProduct>();
  }
}
