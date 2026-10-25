using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using PolicyIssuance.Application.DTO;
using PolicyIssuance.Application.Interfaces;
using Microsoft.Extensions.Http;

namespace PolicyIssuance.Infrastructure.Services;

public class PolicyIntegrationService : IPolicyIntegrationService
{
    private readonly System.Net.Http.IHttpClientFactory _factory;

    public PolicyIntegrationService(
        System.Net.Http.IHttpClientFactory factory)
    {
        _factory = factory;
    }

    private async Task<T?> GetAsync<T>(
        string clientName,
        string endpoint,
        CancellationToken cancellationToken)
    {
        HttpClient client = _factory.CreateClient(clientName);

        using var response = await client.GetAsync(
            endpoint, cancellationToken);

        if (response.StatusCode == HttpStatusCode.NotFound)
            return default;

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<T>(
            cancellationToken: cancellationToken);
    }

    public Task<CustomerIntegrationDto?> GetCustomerAsync(
        int customerId, CancellationToken cancellationToken = default)
        => GetAsync<CustomerIntegrationDto>(
            "Customer", $"api/v1/customers/{customerId}", cancellationToken);

    public Task<KycIntegrationDto?> GetKycAsync(
        int customerId, CancellationToken cancellationToken = default)
        => GetAsync<KycIntegrationDto>(
            "Kyc", $"api/v1/kyc/customer/{customerId}", cancellationToken);

    public Task<ProductIntegrationDto?> GetProductAsync(
        int productId, CancellationToken cancellationToken = default)
        => GetAsync<ProductIntegrationDto>(
            "Product", $"api/v1/products/{productId}", cancellationToken);

    public Task<QuoteIntegrationDto?> GetQuoteAsync(
        int quoteId, CancellationToken cancellationToken = default)
        => GetAsync<QuoteIntegrationDto>(
            "Quote", $"api/v1/quotes/{quoteId}", cancellationToken);

    public Task<UnderwritingIntegrationDto?> GetUnderwritingAsync(
        int quoteId, CancellationToken cancellationToken = default)
        => GetAsync<UnderwritingIntegrationDto>(
            "Underwriting", $"api/v1/underwriting/quote/{quoteId}", cancellationToken);

    public Task<PaymentIntegrationDto?> GetPaymentAsync(
        int quoteId, CancellationToken cancellationToken = default)
        => GetAsync<PaymentIntegrationDto>(
            "Payment", $"api/v1/payments/quote/{quoteId}", cancellationToken);

    public Task RegisterPolicyDocumentAsync(
        int policyId, CancellationToken cancellationToken = default)
        => Task.CompletedTask;

    public Task NotifyPolicyIssuedAsync(
        int policyId, CancellationToken cancellationToken = default)
        => Task.CompletedTask;
}