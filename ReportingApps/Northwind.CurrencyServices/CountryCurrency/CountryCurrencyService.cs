using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Northwind.CurrencyServices.CountryCurrency;

/// <summary>
/// CountryCurrencyService class.
/// </summary>
public class CountryCurrencyService : ICountryCurrencyService
{
    private const string SourceUri = "https://restcountries.eu/rest/v2/name/";
    private HttpClient client = new ();

    /// <summary>
    /// GetLocalCurrencyByCountry method.
    /// </summary>
    /// <param name="countryName">Country name.</param>
    /// <returns>Local currency.</returns>
    public async Task<LocalCurrency> GetLocalCurrencyByCountry(string countryName)
    {
        var uri = new Uri(SourceUri + countryName);
        var stream = await this.client.GetStreamAsync(uri);
        var countryInfo = await JsonSerializer.DeserializeAsync<CountryInformation[]>(stream);

        return new LocalCurrency
        {
            CountryName = countryInfo[0]?.Name,
            CurrencyCode = countryInfo[0]?.Currencies[0]["code"],
            CurrencySymbol = countryInfo[0]?.Currencies[0]["symbol"],
        };
    }
}
