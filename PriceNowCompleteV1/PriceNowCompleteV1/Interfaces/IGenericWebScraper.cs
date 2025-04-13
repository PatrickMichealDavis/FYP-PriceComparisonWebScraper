using PriceNowCompleteV1.Models;

namespace PriceNowCompleteV1.Interfaces
{
    public interface IGenericWebScraper
    {
        Task<decimal> PriceNow(string productUrl);
    }
}
