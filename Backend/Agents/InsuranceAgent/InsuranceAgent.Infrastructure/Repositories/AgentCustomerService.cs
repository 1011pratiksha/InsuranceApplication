
using InsuranceAgent.Application.DTO;
using InsuranceAgent.Application.Interfaces;
using InsuranceAgent.Application.Mapper;
using InsuranceAgent.Domain.Entities;
using InsuranceAgent.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InsuranceAgent.Infrastructure.Repositories
{
    public class AgentCustomerService : IAgentCustomerService
    {
        AppDbContext data;
        CustomerClient customerClient;

        public AgentCustomerService(AppDbContext data, CustomerClient customerClient)
        {
            this.data = data;
            this.customerClient = customerClient;
        }

        public async Task AssignCustomer(int agentId, CreateAgentCustomerDto dto)
        {
            var agent = await data.Agents
                .FirstOrDefaultAsync(x => x.AgentId == agentId);

            if (agent == null)
            {
                throw new Exception("Agent not found.");

            }

            var customer = await customerClient.GetCustomerById(dto.CustomerId);

            if (customer == null)
            {
                throw new Exception("Customer not found.");

            }

            var existing = await data.AgentCustomers.AnyAsync(x => x.CustomerId == dto.CustomerId && x.Status == "Active");


            if (existing)
            {
                throw new Exception("Customer is already assigned to an agent.");

            }

            var agentCustomer = new AgentCustomer
            {
                AgentId = agentId,
                CustomerId = dto.CustomerId,
                AssignedBy = dto.AssignedBy,
                AssignedDate = DateTime.Now,
                Status = "Active",
                CreatedAt = DateTime.Now
            };

            data.AgentCustomers.Add(agentCustomer);
            await data.SaveChangesAsync();
        }

        public async Task<List<GetAgentCustomerDto>> GetAssignedCustomers(int agentId)
        {
            var assignments = await data.AgentCustomers.Where(x => x.AgentId == agentId && x.Status == "Active").ToListAsync();

            var customers = new List<GetAgentCustomerDto>();

            foreach (var x in assignments)
            {
                var customer = await customerClient.GetCustomerById(x.CustomerId);

                customers.Add(new GetAgentCustomerDto
                {
                    AgentCustomerId = x.AgentCustomerId,
                    AgentId = x.AgentId,
                    CustomerId = x.CustomerId,
                    FirstName = customer?.FirstName,
                    LastName = customer?.LastName,
                    Email = customer?.Email,
                    MobileNo = customer?.MobileNumber,
                    AssignedDate = x.AssignedDate,
                    AssignedBy = x.AssignedBy,
                    Status = x.Status,
                    CreatedAt = x.CreatedAt
                });
            }

            return customers;
        }



        public async Task TransferCustomer(int agentId, int customerId, TransferAgentCustomerDto dto)
        {
            var assignment = await data.AgentCustomers.FirstOrDefaultAsync(x => x.AgentId == agentId && x.CustomerId == customerId && x.Status == "Active");

            if (assignment == null)
            {
                throw new Exception("Active customer assignment not found.");
            }

            if (agentId == dto.NewAgentId)
            {
                throw new Exception("Customer is already assigned to this agent.");
            }


            var agent = await data.Agents.FirstOrDefaultAsync(x => x.AgentId == dto.NewAgentId);


            if (agent == null)
            {
                throw new Exception("New agent not found.");
            }


            assignment.Status = "Transferred";

            var newAssignment = new AgentCustomer
            {
                AgentId = dto.NewAgentId,
                CustomerId = customerId,
                AssignedDate = DateTime.Now,
                AssignedBy = dto.AssignedBy,
                Status = "Active",
                CreatedAt = DateTime.Now
            };

            data.AgentCustomers.Add(newAssignment);

            await data.SaveChangesAsync();
        }
    }
}
