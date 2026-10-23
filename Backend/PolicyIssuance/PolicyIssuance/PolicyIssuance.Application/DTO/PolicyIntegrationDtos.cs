namespace PolicyIssuance.Application.DTO;

public class CustomerIntegrationDto
{
    public int CustomerId { get; set; }
    public string FullName { get; set; } = "";
}

public class KycIntegrationDto
{
    public int CustomerId { get; set; }
    public string Status { get; set; } = "";
}

public class ProductIntegrationDto
{
    public int ProductId { get; set; }
    public string Status { get; set; } = "";
}

public class QuoteIntegrationDto
{
    public int QuoteId { get; set; }
    public int CustomerId { get; set; }
    public int ProductId { get; set; }
    public decimal PremiumAmount { get; set; }
    public string Status { get; set; } = "";
}

public class UnderwritingIntegrationDto
{
    public int QuoteId { get; set; }
    public string Decision { get; set; } = "";
}

public class PaymentIntegrationDto
{
    public int QuoteId { get; set; }
    public string Status { get; set; } = "";
}