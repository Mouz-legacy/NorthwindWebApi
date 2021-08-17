using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Northwind.CurrencyServices.CurrencyExchange
{
    /// <summary>
    /// CurrencyJsonInfo to store info from json file from currency exchange service. 
    /// </summary>
    public class CurrencyJsonInfo
    {
        /// <summary>
        /// Gets or sets Service property.
        /// </summary>
        [JsonPropertyName("source")]
        public string Source { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets quotes property.
        /// </summary>
        [JsonPropertyName("quotes")]
        public Dictionary<string, decimal> Quotes { get; set; } = new ();
    }
}
