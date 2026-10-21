
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReportsAndDashboard.Domain.Entities
{
  public class State
  {
    [Key]
    public long StateId { get; set; }

    public long CountryId { get; set; }

    public string StateName { get; set; } = string.Empty;

    // Foreign key relationship

    [ForeignKey(nameof(CountryId))]
    public Country Country { get; set; } = null!;

    // Navigation collection

    public List<City> Cities { get; set; }
        = new List<City>();
  }
}
