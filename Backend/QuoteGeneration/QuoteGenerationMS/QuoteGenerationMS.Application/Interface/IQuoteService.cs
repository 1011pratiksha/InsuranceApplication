using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
