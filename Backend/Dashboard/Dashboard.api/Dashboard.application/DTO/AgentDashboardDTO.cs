namespace Dashboard.application.DTO
{
    public class AgentDashboardDTO
    {
        public int TotalCustomers { get; set; }
        public int PoliciesSold { get; set; }
        public decimal CommissionPending { get; set; }
        public decimal CommissionPaidYTD { get; set; }
    }
}