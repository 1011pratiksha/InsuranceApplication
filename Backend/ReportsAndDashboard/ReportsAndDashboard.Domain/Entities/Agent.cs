
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReportsAndDashboard.Domain.Entities
{
  [Table("agent_master")]
  public class Agent
  {
    [Key]
    public long AgentId { get; set; }

    [Required]
    [MaxLength(30)]
    public string AgentCode { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; } = string.Empty;

    [MaxLength(100)]
    public string? LastName { get; set; }

    [MaxLength(150)]
    public string? Email { get; set; }

    [MaxLength(20)]
    public string? Phone { get; set; }

    public DateTime JoiningDate { get; set; }

    [Required]
    [MaxLength(20)]
    public string Status { get; set; } = "Active";
  }
}
