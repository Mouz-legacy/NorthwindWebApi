using System.Threading.Tasks;

namespace Northwind.CurrencyServices.CountryCurrency
{
    /// <summary>
    /// ICountryCurrencyService interface.
    /// </summary>
    public interface ICountryCurrencyService
    {
        /// <summary>
        /// GetLocalCurrencyByCountry method.
        /// </summary>
        /// <param name="countryName">Country name.</param>
        /// <returns>Return local currency.</returns>
        Task<LocalCurrency> GetLocalCurrencyByCountry(string countryName);
    }
}
