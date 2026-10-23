
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReportsAndDashboard.Domain.Entities
{
  public class City
  {
    [Key]
    public long CityId { get; set; }

    public long StateId { get; set; }

    public string CityName { get; set; } = string.Empty;

    // Foreign key relationship

    [ForeignKey(nameof(StateId))]
    public State State { get; set; } = null!;

    // Navigation collection

    public List<CustomerAddress> CustomerAddresses { get; set; }
        = new List<CustomerAddress>();
  }
}
