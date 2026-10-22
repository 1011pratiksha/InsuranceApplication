namespace InsuranceAgent.Application.DTO
{
    public class CreateAgentDto
    {
        public int UserId { get; set; }

        public DateTime JoiningDate { get; set; }

        public string? LicenseNumber { get; set; }
    }
}