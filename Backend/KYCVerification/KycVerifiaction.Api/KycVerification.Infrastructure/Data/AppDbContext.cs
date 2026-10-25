using KycVerification.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KycVerification.Infrastructure.Data
{
    public class AppDbContext: DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }


        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerKyc> CustomerKycs { get; set; }

        public DbSet<KycDocument> KycDocuments { get; set; }

        public DbSet<CkycDetails> CkycDetails { get; set; }

        public DbSet<KycVerificationLog> KycVerificationLogs { get; set; }

        public DbSet<DocumentTypeMaster> DocumentTypeMasters { get; set; }

    }
}
