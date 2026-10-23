
using System.ComponentModel.DataAnnotations;

namespace ReportsAndDashboard.Domain.Entities
{
  public class DocumentType
  {
    [Key]
    public long DocumentTypeId { get; set; }

    public string DocumentName { get; set; } = string.Empty;
  }
}
