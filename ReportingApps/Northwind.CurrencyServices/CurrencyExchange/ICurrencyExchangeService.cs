using System.Threading.Tasks;

namespace Northwind.CurrencyServices.CurrencyExchange
{
    /// <summary>
    /// ICurrencyExchangeService interface.
    /// </summary>
    public interface ICurrencyExchangeService
    {
        /// <summary>
        /// GetCurrencyExchangeRate method.
        /// </summary>
        /// <param name="baseCurrency">Base currency.</param>
        /// <param name="exchangeCurrency">Exchange currency.</param>
        /// <returns>Decimal value.</returns>
        Task<decimal> GetCurrencyExchangeRate(string baseCurrency, string exchangeCurrency);
    }
}
