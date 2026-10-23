
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReportsAndDashboard.Domain.Entities
{
  public class Country
  {
    [Key]
    public long CountryId { get; set; }

    public string CountryName { get; set; } = string.Empty;

    // Navigation collection

    public List<State> States { get; set; }
        = new List<State>();
  }
}
