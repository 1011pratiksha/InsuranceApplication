namespace InsuranceAgent.Application.DTO
{
    public class GetAgentDto
    {
        public int AgentId { get; set; }

        public int UserId { get; set; }

        public string AgentCode { get; set; } 

        public string UserName { get; set; } 

        public string Email { get; set; } 

        public string MobileNo { get; set; } 

        public DateTime JoiningDate { get; set; }

        public string? LicenseNumber { get; set; }

        public string Status { get; set; } 

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}