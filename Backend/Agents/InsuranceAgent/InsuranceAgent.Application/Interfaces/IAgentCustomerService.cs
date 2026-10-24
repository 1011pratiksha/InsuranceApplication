using InsuranceAgent.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAgent.Application.Interfaces
{
    public interface IAgentCustomerService
    {
        Task AssignCustomer(int agentId, CreateAgentCustomerDto dto, int assignedBy);

        Task<List<GetAgentCustomerDto>> GetAssignedCustomers(int agentId);

        Task TransferCustomer(int agentId, int customerId, TransferAgentCustomerDto dto, int assignedBy);
        Task<List<CustomerDTO>?> GetAllCustomers();
    }
}
