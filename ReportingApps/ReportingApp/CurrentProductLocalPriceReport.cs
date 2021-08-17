using System;
using System.Threading.Tasks;
using Northwind.CurrencyServices.CountryCurrency;
using Northwind.CurrencyServices.CurrencyExchange;
using Northwind.ReportingSerivces.ProductReports;

namespace ReportingApp
{
    /// <summary>
    /// CurrentProductLocalPriceReport class.
    /// </summary>
    public class CurrentProductLocalPriceReport
    {
        private readonly IProductReportService productReportService;
        private readonly ICurrencyExchangeService currencyExchangeService;
        private readonly ICountryCurrencyService countryCurrencyService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CurrentProductLocalPriceReport"/> class.
        /// </summary>
        /// <param name="productReportService">Report service.</param>
        /// <param name="currencyExchangeService">Exchange service.</param>
        /// <param name="countryCurrencyService">Currency service.</param>
        public CurrentProductLocalPriceReport(IProductReportService productReportService, ICurrencyExchangeService currencyExchangeService, ICountryCurrencyService countryCurrencyService)
        {
            this.productReportService = productReportService ?? throw new ArgumentNullException(nameof(productReportService));
            this.currencyExchangeService = currencyExchangeService ?? throw new ArgumentNullException(nameof(currencyExchangeService));
            this.countryCurrencyService = countryCurrencyService ?? throw new ArgumentNullException(nameof(countryCurrencyService));
        }

        /// <summary>
        /// PrintReport method.
        /// </summary>
        /// <returns>Task with report.</returns>
        public Task PrintReport()
        {
            var productReport =
                this.productReportService.GetCurrentProductsWithLocalCurrencyReport(
                    this.countryCurrencyService,
                    this.currencyExchangeService).Result;
            foreach (var reportLine in productReport.Products)
            {
                Console.WriteLine("{0}, {1}, {2}, {3}, {4}", reportLine.Name, (int)reportLine.Price, reportLine.Country, (int)reportLine.LocalPrice, reportLine.CurrencySymbol);
            }

            return Task.CompletedTask;
        }
    }
}
