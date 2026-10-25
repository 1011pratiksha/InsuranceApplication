
using QuoteGenerationMS.Application.DTO;

namespace QuoteGenerationMS.Application.Interface
{
    public interface IQuoteService
    {
        Task CreateQuote(createQuoteDTO dto);

        Task<List<quoteDTO>> GetAllQuotes();

        Task<quoteDTO?> GetQuoteById(int id);

        Task UpdateQuote(int id, updateQuoteDTO dto);

        Task CancelQuote(int id);

        Task<int> SendQuoteForUnderwriting(
            int quoteId,
            int customerId);
    }
}
