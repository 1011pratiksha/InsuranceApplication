
namespace InsuranceAgent.Application.DTO
{
    public class GetAgentCustomerDto
    {
        public int AgentCustomerId { get; set; }

        public int AgentId { get; set; }

        public int CustomerId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string? Email { get; set; }

        public string? MobileNo { get; set; }

        public DateTime AssignedDate { get; set; }

        public int AssignedBy { get; set; }

        public string Status { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
