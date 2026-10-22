using InsuranceAgent.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAgent.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }


        //My Actual Models
        public DbSet<Agent> Agents { get; set; }

        public DbSet<AgentCustomer> AgentCustomers { get; set; }

        public DbSet<AgentPolicy> AgentPolicies { get; set; }

        public DbSet<CommissionRule> CommissionRules { get; set; }

        public DbSet<AgentCommission> AgentCommissions { get; set; }

        public DbSet<AgentCommissionPayment> AgentCommissionPayments { get; set; }



    }
}
