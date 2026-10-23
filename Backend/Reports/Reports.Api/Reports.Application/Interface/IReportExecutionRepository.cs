using Reports.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reports.Application.Interface
{
   public interface IReportExecutionRepository
    {
        Task AddAsync(ReportExecution execution);

        Task SaveChangesAsync();
    }
}
