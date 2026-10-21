using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PolicyIssuance.Application.DTO
{
    public class CreateEndorsementDto
    {
        [Required]
        public string EndorsementType { get; set; } = string.Empty;

        public string? OldValue { get; set; }

        public string? NewValue { get; set; }
    }
}
