using System.Collections.Generic;
using System.Text.Json.Serialization;

#pragma warning disable CA1819

namespace Northwind.CurrencyServices.CountryCurrency
{
    /// <summary>
    /// CountryInformation class.
    /// </summary>
    public class CountryInformation
    {
        /// <summary>
        /// Gets or sets country name.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets Currencies.
        /// </summary>
        [JsonPropertyName("currencies")]
        public Dictionary<string, string>[] Currencies { get; set; }
    }
}
