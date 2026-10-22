namespace InsuranceAgent.Application.DTO
{
    public class AgentPolicyResponseDto
    {
        public int AgentPolicyId { get; set; }

        public int AgentId { get; set; }

        public int PolicyId { get; set; }

        public int CustomerId { get; set; }

        public DateTime AssignedDate { get; set; }

        public DateTime? SaleDate { get; set; }

        public string Status { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}