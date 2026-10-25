
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PolicyServicing.Domain.Entities
{
    [Table("PolicyEndorsements", Schema = "dbo")]
    public class PolicyEndorsement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("EndorsementId")]
        public int endorsement_id { get; set; }

        [Column("PolicyId")]
        public int policy_id { get; set; }

        [Required]
        [Column("EndorsementType")]
        public string endorsement_type { get; set; } = string.Empty;

        [Column("OldValue")]
        public string? previous_value { get; set; }

        [Column("NewValue")]
        public string? updated_value { get; set; }

        [Column("ApprovedBy")]
        public int? approved_by { get; set; }

        [Required]
        [Column("EndorsementDate")]
        public DateTime endorsement_date { get; set; }

        public string approvel_status { get; set; }
    }
}
