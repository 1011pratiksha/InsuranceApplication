using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CustomerRegistration.Domain.Entities
{
    public class CustomerDocument
    {
        [Key]
        public int DocumentId { get; set; }
        [Required]
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }

        [Required]
        public string DocumentType { get; set; } 

        [Required]
        public string FileName { get; set; } 

        [Required]
        public string FilePath { get; set; } 

        [Required]
        public DateTime UploadedDate { get; set; }

        [Required]
        public string VerificationStatus { get; set; }
    }
}
