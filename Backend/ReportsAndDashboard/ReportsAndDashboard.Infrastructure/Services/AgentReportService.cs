
using Microsoft.EntityFrameworkCore;
using ReportsAndDashboard.Application.DTO;
using ReportsAndDashboard.Application.Interfaces;
using InsuranceApp.Infrastructure.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportsAndDashboard.Infrastructure.Services
{
  public class AgentReportService : IAgentReportService
  {
    private readonly AppDbContext db;

    public AgentReportService(AppDbContext db)
    {
      this.db = db;
    }

    public async Task<List<AgentReportDto>> GetAgentReportAsync()
    {
      return await db.Agents
         
          .Select(agent => new AgentReportDto
          {
            AgentId = agent.AgentId,
            AgentCode = agent.AgentCode,
            FirstName = agent.FirstName,
            LastName = agent.LastName,
            Email = agent.Email,
            Phone = agent.Phone,
            JoiningDate = agent.JoiningDate,
            Status = agent.Status
          })
          .ToListAsync();
    }
  }
}
