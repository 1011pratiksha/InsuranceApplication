
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReportsAndDashboard.Domain.Entities
{
  public class InsuranceProduct
  {
    [Key]
    public long ProductId { get; set; }

    public long CategoryId { get; set; }

    public string ProductCode { get; set; } = string.Empty;

    public string ProductName { get; set; } = string.Empty;

    public string ProductStatus { get; set; } = "Active";

    // Foreign key relationship

    [ForeignKey(nameof(CategoryId))]
    public ProductCategory Category { get; set; } = null!;

    // Navigation collection

    public List<Policy> Policies { get; set; }
        = new List<Policy>();
  }
}
