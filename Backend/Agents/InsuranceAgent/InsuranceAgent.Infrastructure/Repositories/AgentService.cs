
using InsuranceAgent.Application.DTO;
using InsuranceAgent.Application.Interfaces;
using InsuranceAgent.Domain.Entities;
using InsuranceAgent.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InsuranceAgent.Infrastructure.Repositories
{
    public class AgentService : IAgentService
    {
        AppDbContext data;

        public AgentService(AppDbContext data)
        {
            this.data = data;
        }

        public async Task CreateAgent(CreateAgentDto dto)
        {
            var user = await data.Users.FindAsync(dto.UserId);

            if (user == null)
            {
                throw new Exception("User not found.");
            }

            var agent = new Agent
            {
                UserId = dto.UserId,
                AgentCode = "AG00" + (await data.Agents.CountAsync() + 1),
                UserName = user.UserName,
                Email = user.Email,
                MobileNo = user.Mobile,
                JoiningDate = dto.JoiningDate,
                LicenseNumber = dto.LicenseNumber,
                Status = "Pending",
                CreatedAt = DateTime.Now
            };

            data.Agents.Add(agent);

            await data.SaveChangesAsync();
        }

        public async Task<GetAgentDto?> GetAgentById(int id)
        {
            var find = await data.Agents.FindAsync(id);

            if (find == null)
            {
                return null;
            }

            var d = new GetAgentDto
            {
                AgentId = find.AgentId,
                UserId = find.UserId,
                AgentCode = find.AgentCode,
                UserName = find.UserName,
                Email = find.Email,
                MobileNo = find.MobileNo,
                JoiningDate = find.JoiningDate,
                LicenseNumber = find.LicenseNumber,
                Status = find.Status,
                CreatedAt = find.CreatedAt,
                UpdatedAt = find.UpdatedAt
            };

            return d;
        }

        public async Task<List<GetAgentDto>> GetAllAgents()
        {
            var agents = await data.Agents.ToListAsync();

            var list = agents.Select(a => new GetAgentDto
            {
                AgentId = a.AgentId,
                UserId = a.UserId,
                AgentCode = a.AgentCode,
                UserName = a.UserName,
                Email = a.Email,
                MobileNo = a.MobileNo,
                JoiningDate = a.JoiningDate,
                LicenseNumber = a.LicenseNumber,
                Status = a.Status,
                CreatedAt = a.CreatedAt,
                UpdatedAt = a.UpdatedAt
            }).ToList();

            return list;
        }

        public async Task UpdateAgent(int id, UpdateAgentDto dto)
        {
            var agent = await data.Agents.FindAsync(id);

            if (agent == null)
            {
                throw new Exception("Agent not found.");
            }

            agent.JoiningDate = dto.JoiningDate;
            agent.LicenseNumber = dto.LicenseNumber;
            agent.UpdatedAt = DateTime.Now;

            await data.SaveChangesAsync();
        }
    }
}
