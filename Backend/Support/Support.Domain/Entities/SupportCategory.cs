
using System.ComponentModel.DataAnnotations;

namespace Support.Domain.Entities
{
  public class SupportCategory
  {
    [Key]
    public int CategoryId { get; set; }

    [Required]
    [StringLength(50)]
    public string CategoryName { get; set; } = string.Empty;

    [StringLength(200)]
    public string? Description { get; set; }

    public bool IsActive { get; set; } = true;
  }
}
