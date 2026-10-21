
using System.ComponentModel.DataAnnotations;

namespace ReportsAndDashboard.Domain.Entities
{
  public class Relationship
  {
    [Key]
    public long RelationshipId { get; set; }

    public string RelationshipName { get; set; } = string.Empty;
  }
}
