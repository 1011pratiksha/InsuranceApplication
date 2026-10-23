
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReportsAndDashboard.Domain.Entities
{
  public class CustomerAddress
  {
    [Key]
    public long CustomerAddressId { get; set; }

    public long CustomerId { get; set; }

    public string AddressType { get; set; } = string.Empty;

    public string AddressLine1 { get; set; } = string.Empty;

    public string? AddressLine2 { get; set; }

    public string District { get; set; } = string.Empty;

    public long CityId { get; set; }

    public string Pincode { get; set; } = string.Empty;

    public bool IsCurrent { get; set; }

    // Foreign key relationships

    [ForeignKey(nameof(CustomerId))]
    public Customer Customer { get; set; } = null!;

    [ForeignKey(nameof(CityId))]
    public City City { get; set; } = null!;
  }
}
