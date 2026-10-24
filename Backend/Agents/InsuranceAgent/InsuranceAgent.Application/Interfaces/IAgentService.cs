using InsuranceAgent.Application.DTO;
using InsuranceAgent.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAgent.Application.Interfaces
{
    public interface IAgentService
    {

      
        Task CreateAgent(CreateAgentDto dto);

        Task<List<GetAgentDto>> GetAllAgents();

        Task<GetAgentDto> GetAgentById(int id);

        Task UpdateAgent(int id, UpdateAgentDto dto);

        Task<List<UserDTO>> GetAllUsers();


    }
}
