using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Northwind.ReportingServices.OData.ProductReports;
using Northwind.ReportingServices.ProductReports;
using Northwind.ReportingServices.SqlService;

namespace ReportingApp
{
    /// <summary>
    /// Program class.
    /// </summary>
    public sealed class Program
    {
        private const string NorthwindServiceUrl = "https://services.odata.org/V3/Northwind/Northwind.svc";
        private static readonly IReadOnlyDictionary<string, string> Services = new Dictionary<string, string>
        {
            ["CurrentProductsReport"] = "current-products",
            ["MostExpensiveProductsReport"] = "most-expensive-products",
            ["PriceLessThenProducts"] = "price-less-then-products",
            ["PriceBetweenProducts"] = "price-between-products",
            ["PriceAboveAverageProducts"] = "price-above-average-products",
            ["UnitsInStockDeficit"] = "units-in-stock-deficit",
            ["PriceMoreThenProducts"] = "price-more-then-products",
            ["UnitsInStockProficit"] = "units-in-stock-proficit",
            ["CurrentProductsLocalPrices"] = "current-products-local-prices",
        };

        /// <summary>
        /// A program entry point.
        /// </summary>
        /// <param name="args">Program arguments.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public static async Task Main(string[] args)
        {
            if (args == null || args.Length < 1)
            {
                ShowHelp();
                return;
            }

            var reportName = args[0];

            if (string.Equals(reportName, Services["CurrentProductsReport"], StringComparison.InvariantCultureIgnoreCase))
            {
                await ShowCurrentProducts();
                return;
            }
            else if (string.Equals(reportName, Services["MostExpensiveProductsReport"], StringComparison.InvariantCultureIgnoreCase))
            {
                if (args.Length > 1 && int.TryParse(args[1], out int count))
                {
                    await ShowMostExpensiveProducts(count);
                    return;
                }
            }
            else if (string.Equals(reportName, Services["PriceLessThenProducts"], StringComparison.InvariantCultureIgnoreCase))
            {
                if (args.Length > 1 && int.TryParse(args[1], out int count))
                {
                    await ShowPriceLessThenProducts(count);
                    return;
                }
            }
            else if (string.Equals(reportName, Services["PriceBetweenProducts"], StringComparison.InvariantCultureIgnoreCase))
            {
                if (args.Length > 2 && int.TryParse(args[1], out int leftSide) &&
                    int.TryParse(args[2], out int rightSide))
                {
                    await ShowPriceBetweenProducts(leftSide, rightSide);
                    return;
                }
            }
            else if (string.Equals(reportName, Services["PriceAboveAverageProducts"], StringComparison.InvariantCultureIgnoreCase))
            {
                await ShowPriceAboveAverageProducts();
                return;
            }
            else if (string.Equals(reportName, Services["UnitsInStockDeficit"], StringComparison.InvariantCultureIgnoreCase))
            {
                await ShowUnitsInStockDeficit();
                return;
            }
            else if (string.Equals(reportName, Services["UnitsInStockProficit"], StringComparison.InvariantCultureIgnoreCase))
            {
                await ShowUnitsInStockProficit();
                return;
            }
            else if (string.Equals(reportName, Services["PriceMoreThenProducts"], StringComparison.InvariantCultureIgnoreCase))
            {
                if (args.Length > 1 && int.TryParse(args[1], out int count))
                {
                    await ShowPriceMoreThenProducts(count);
                    return;
                }
            }
            else if (string.Equals(reportName, Services["CurrentProductsLocalPrices"], StringComparison.InvariantCultureIgnoreCase))
            {
                await ShowCurrentProductsLocalPrices();
                return;
            }
            else
            {
                ShowHelp();
            }
        }

        private static void ShowHelp()
        {
            Console.WriteLine("Usage:");
            Console.WriteLine("\tReportingApp.exe <report> <report-argument1> <report-argument2> ...");
            Console.WriteLine();
            Console.WriteLine("Reports:");
            Console.WriteLine($"\t{Services["CurrentProductsReport"]}\t\tShows current products.");
            Console.WriteLine($"\t{Services["MostExpensiveProductsReport"]}\t\tShows specified number of the most expensive products.");
        }

        private static async Task ShowCurrentProducts()
        {
            var service = new ProductReportService(new Uri(NorthwindServiceUrl));
            var report = await service.GetCurrentProductsReport();
            PrintProductReport("current products:", report);
        }

        private static async Task ShowMostExpensiveProducts(int count)
        {
            var service = new ProductReportService(new Uri(NorthwindServiceUrl));
            var report = await service.GetMostExpensiveProductsReport(count);
            PrintProductReport($"{count} most expensive products:", report);
        }

        private static async Task ShowPriceLessThenProducts(int count)
        {
            var service = new ProductReportService(new Uri(NorthwindServiceUrl));
            var report = await service.GetPriceLessThenProductsReport(count);
            PrintProductReport($"Report - products with price less then {count}", report);
        }

        private static async Task ShowPriceBetweenProducts(int leftSide, int rightSide)
        {
            var service = new ProductReportService(new Uri(NorthwindServiceUrl));
            var report = await service.GetPriceBetweenProductsReport(leftSide, rightSide);
            PrintProductReport($"ReportingApp.exe price-between-products {leftSide} and {rightSide}", report);
        }

        private static async Task ShowUnitsInStockDeficit()
        {
            var service = new ProductReportService(new Uri(NorthwindServiceUrl));
            var report = await service.GetUnitsInStockDeficit();
            PrintProductReport($"ReportingApp.exe units-in-stock-deficit ", report);
        }

        private static async Task ShowPriceMoreThenProducts(int count)
        {
            var service = new ProductReportService(new Uri(NorthwindServiceUrl));
            var report = await service.GetPriceMoreThenProductsReport(count);
            PrintProductReport($"ReportingApp.exe price-more-then-count {count} ", report);
        }

        private static async Task ShowUnitsInStockProficit()
        {
            var service = new ProductReportService(new Uri(NorthwindServiceUrl));
            var report = await service.GetUnitsInStockProficit();
            PrintProductReport($"ReportingApp.exe units-in-stock-proficit ", report);
        }

        private static async Task ShowPriceAboveAverageProducts()
        {
            var service = new ProductReportService(new Uri(NorthwindServiceUrl));
            var report = await service.GetPriceAboveAverageProductsReport();
            PrintProductReport($"ReportingApp.exe price-above-average ", report);
        }

        private static async Task ShowCurrentProductsLocalPrices()
        {
            var service = new ProductReportService(new Uri(NorthwindServiceUrl));
            var report = await service.GetCurrentProductsWithLocalCurrencyReport(new CountryCurrencyService(), new CurrencyExchangeService("0e49fdd493f588d9d287127c8f992469"));
            PrintProductLocalReport($"ReportingApp.exe current-products-local-prices ", report);
        }

        private static void PrintProductReport(string header, ProductReport<ProductPrice> productReport)
        {
            Console.WriteLine($"Report - {header}");
            foreach (var reportLine in productReport.Products)
            {
                Console.WriteLine("{0}, {1}", reportLine.Name, reportLine.Price);
            }
        }

        private static void PrintProductLocalReport(string header, ProductReport<ProductLocalPrice> productReport)
        {
            Console.WriteLine($"Report - {header}");
            foreach (var reportLine in productReport.Products)
            {
                Console.WriteLine("{0}, {1}, {2}, {3}, {4}", reportLine.Name, (int)reportLine.Price, reportLine.Country, (int)reportLine.LocalPrice, reportLine.CurrencySymbol);
            }
        }
    }
}
