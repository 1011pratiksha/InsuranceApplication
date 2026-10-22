
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ReportsAndDashboard.Application.DTO;

namespace ReportsAndDashboard.Application.Interfaces
{
  public interface ICustomerReportService
  {
    Task<List<CustomerReportDto>> GetCustomerReportAsync();
  }
}
