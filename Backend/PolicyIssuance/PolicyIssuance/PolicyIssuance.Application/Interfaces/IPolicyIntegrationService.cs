using PolicyIssuance.Application.DTO;

namespace PolicyIssuance.Application.Interfaces;

public interface IPolicyIntegrationService
{
    Task<CustomerIntegrationDto?> GetCustomerAsync(
        int customerId, CancellationToken cancellationToken = default);

    Task<KycIntegrationDto?> GetKycAsync(
        int customerId, CancellationToken cancellationToken = default);

    Task<ProductIntegrationDto?> GetProductAsync(
        int productId, CancellationToken cancellationToken = default);

    Task<QuoteIntegrationDto?> GetQuoteAsync(
        int quoteId, CancellationToken cancellationToken = default);

    Task<UnderwritingIntegrationDto?> GetUnderwritingAsync( 
        int quoteId, CancellationToken cancellationToken = default);

    Task<PaymentIntegrationDto?> GetPaymentAsync(
        int quoteId, CancellationToken cancellationToken = default);

    Task RegisterPolicyDocumentAsync(
        int policyId, CancellationToken cancellationToken = default);

    Task NotifyPolicyIssuedAsync(
        int policyId, CancellationToken cancellationToken = default);
}