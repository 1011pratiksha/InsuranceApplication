using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyIssuance.Application.DTO
{
    public class PolicyDocumentDto
    {
        public int DocumentId { get; set; }

        public int PolicyId { get; set; }

        public string DocumentName { get; set; } = string.Empty;

        public string FilePath { get; set; } = string.Empty;

        public int VersionNo { get; set; }

        public DateTime GeneratedDate { get; set; }

        public byte[]? FileBytes { get; set; }
    }
}
