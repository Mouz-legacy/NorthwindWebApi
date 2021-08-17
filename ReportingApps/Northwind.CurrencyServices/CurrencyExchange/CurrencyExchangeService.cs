using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Northwind.CurrencyServices.CurrencyExchange;

/// <summary>
/// CurrencyExchangeService class.
/// </summary>
public class CurrencyExchangeService : ICurrencyExchangeService
{
    private static readonly HttpClient Client = new ();
    private static CurrencyJsonInfo currencyJsonInfo;
    private readonly string accessKey;
    private readonly string source = "http://api.currencylayer.com/live?access_key=";

    /// <summary>
    /// Initializes a new instance of the <see cref="CurrencyExchangeService"/> class.
    /// </summary>
    /// <param name="accessKey">Access key.</param>
    public CurrencyExchangeService(string accessKey)
    {
        this.accessKey = !string.IsNullOrWhiteSpace(accessKey) ? accessKey : throw new ArgumentException("Access key is invalid.", nameof(accessKey));
    }

    /// <summary>
    /// GetCurrencyExchangeRate method.
    /// </summary>
    /// <param name="baseCurrency">Base currency.</param>
    /// <param name="exchangeCurrency">Exchange currency.</param>
    /// <returns>Task decimal values.</returns>
    public async Task<decimal> GetCurrencyExchangeRate(string baseCurrency, string exchangeCurrency)
    {
        if (currencyJsonInfo is null)
        {
            currencyJsonInfo = await this.GetCurrencyRateInfo(baseCurrency, exchangeCurrency);
        }

        return CurrencyExchangeService.currencyJsonInfo.Quotes[baseCurrency + exchangeCurrency];
    }

    private async Task<CurrencyJsonInfo> GetCurrencyRateInfo(string baseCurrency, string exchangeCurrency)
    {
        var stream = await Client.GetStreamAsync(this.source + this.accessKey + "&source" + baseCurrency);

        return await JsonSerializer.DeserializeAsync<CurrencyJsonInfo>(stream);
    }
}
