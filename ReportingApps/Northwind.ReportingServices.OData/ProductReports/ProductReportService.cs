using System;
using System.Collections.Generic;
using System.Data.Services.Client;
using System.Linq;
using System.Threading.Tasks;
using Northwind.CurrencyServices.CountryCurrency;
using Northwind.CurrencyServices.CurrencyExchange;
using Northwind.ReportingSerivces.ProductReports;
using Northwind.ReportingServices.ProductReports;
using NorthwindModel;

namespace Northwind.ReportingServices.OData.ProductReports
{
    /// <summary>
    /// Represents a service that produces product-related reports.
    /// </summary>
    public class ProductReportService : IProductReportService
    {
        private readonly NorthwindEntities entities;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductReportService"/> class.
        /// </summary>
        /// <param name="northwindServiceUri">An URL to Northwind OData service.</param>
        public ProductReportService(Uri northwindServiceUri)
        {
            this.entities = new NorthwindModel.NorthwindEntities(northwindServiceUri ?? throw new ArgumentNullException(nameof(northwindServiceUri)));
        }

        /// <summary>
        /// Gets a product report with all current products.
        /// </summary>
        /// <returns>Returns <see cref="ProductReport{T}"/>.</returns>
        public async Task<ProductReport<ProductPrice>> GetCurrentProductsReport()
        {
            DataServiceQueryContinuation<ProductPrice> token = null;

            var query = (DataServiceQuery<ProductPrice>)(
                from p in this.entities.Products
                where !p.Discontinued
                orderby p.ProductName
                select new ProductPrice
                {
                    Name = p.ProductName,
                    Price = p.UnitPrice ?? 0,
                });
            var result = new List<ProductPrice>();
            try
            {
                var response = await Task<QueryOperationResponse<ProductPrice>>.Factory
                    .FromAsync(query.BeginExecute(null, null), (iar) => query.EndExecute(iar) as QueryOperationResponse<ProductPrice>)
                    .ConfigureAwait(false);
                do
                {
                    if (token != null)
                    {
                        try
                        {
                            response = await Task<QueryOperationResponse<ProductPrice>>.Factory
                                .FromAsync(this.entities.BeginExecute(token, null, null), (iar)
                                    => this.entities.EndExecute<ProductPrice>(iar) as QueryOperationResponse<ProductPrice>)
                                .ConfigureAwait(false);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            throw;
                        }
                    }

                    result.AddRange(response);
                }
                while ((token = response.GetContinuation()) != null);
            }
            catch (DataServiceQueryException ex)
            {
                throw new ApplicationException(
                    "An error occurred during query execution.", ex);
            }

            return new ProductReport<ProductPrice>(result);
        }

        /// <summary>
        /// Gets a product report with most expensive products.
        /// </summary>
        /// <param name="count">Items count.</param>
        /// <returns>Returns <see cref="ProductReport{ProductPrice}"/>.</returns>
        public async Task<ProductReport<ProductPrice>> GetMostExpensiveProductsReport(int count)
        {
            var query = (DataServiceQuery<ProductPrice>)this.entities.Products.
                Where(p => p.UnitPrice != null).
                OrderByDescending(p => p.UnitPrice.Value).
                Take(count).
                Select(p => new ProductPrice { Name = p.ProductName, Price = p.UnitPrice ?? 0 });

            var result = await Task<IEnumerable<ProductPrice>>.Factory.FromAsync(query.BeginExecute(null, null), (ar) =>
            {
                return query.EndExecute(ar);
            });

            return new ProductReport<ProductPrice>(result);
        }

        /// <summary>
        /// Gets a product report with products, which price is less then products count.
        /// </summary>
        /// <param name="count">Items count.</param>
        /// <returns>Returns <see cref="ProductReport{ProductPrice}"/>.</returns>
        public async Task<ProductReport<ProductPrice>> GetPriceLessThenProductsReport(int count)
        {
            var query = (DataServiceQuery<ProductPrice>)this.entities.Products.Where(p => p.UnitPrice.Value < count)
                .OrderByDescending(p => p.UnitPrice.Value)
                .Select(p => new ProductPrice() { Name = p.ProductName, Price = p.UnitPrice ?? 0 });

            var result = await Task<IEnumerable<ProductPrice>>.Factory.FromAsync(query.BeginExecute(null, null), (ar) =>
            {
                return query.EndExecute(ar);
            });

            return new ProductReport<ProductPrice>(result);
        }

        /// <summary>
        /// Gets a product report with products, which price is between left and right operands.
        /// </summary>
        /// <param name="leftSide">Left side operand.</param>
        /// <param name="rightSide">Right side operand.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public async Task<ProductReport<ProductPrice>> GetPriceBetweenProductsReport(int leftSide, int rightSide)
        {
            var query = (DataServiceQuery<ProductPrice>)(from p in this.entities.Products
             where p.UnitPrice.Value <= rightSide && p.UnitPrice.Value >= leftSide
             select new ProductPrice()
             {
                 Name = p.ProductName,
                 Price = p.UnitPrice ?? 0,
             });

            var result = await Task<IEnumerable<ProductPrice>>.Factory.FromAsync(query.BeginExecute(null, null), (ar) =>
            {
                return query.EndExecute(ar);
            });

            return new ProductReport<ProductPrice>(result);
        }

        /// <summary>
        /// Gets a product report with products, which price is above average.
        /// </summary>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public async Task<ProductReport<ProductPrice>> GetPriceAboveAverageProductsReport()
        {
            var products = (DataServiceQuery<ProductPrice>)this.entities.Products.Select(p => new ProductPrice
            {
                Name = p.ProductName,
                Price = p.UnitPrice ?? 0,
            });
            var resultProducts = await Task<IEnumerable<ProductPrice>>.Factory.FromAsync(
                products.BeginExecute(null, null),
                (ar) =>
                {
                    return products.EndExecute(ar);
                });
            decimal priceSum = 0;
            int countProducts = 0;
            foreach (var prodcut in resultProducts)
            {
                priceSum += prodcut.Price;
                countProducts++;
            }

            var query = (DataServiceQuery<ProductPrice>)this.entities.Products.Where(p => p.UnitPrice > priceSum / countProducts)
                .OrderBy(p => p.UnitPrice)
                .Select(p => new ProductPrice() { Name = p.ProductName, Price = p.UnitPrice ?? 0 });

            var result = await Task<IEnumerable<ProductPrice>>.Factory.FromAsync(query.BeginExecute(null, null), (ar) =>
            {
                return query.EndExecute(ar);
            });

            return new ProductReport<ProductPrice>(result);
        }

        /// <summary>
        /// Gets a product report with products, which amount unit in stock is less then units on order.
        /// </summary>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public async Task<ProductReport<ProductPrice>> GetUnitsInStockDeficit()
        {
            var query = (DataServiceQuery<ProductPrice>)(from p in this.entities.Products
                    where p.UnitsInStock.Value < p.UnitsOnOrder.Value
                    select new ProductPrice
                    {
                        Name = p.ProductName,
                        Price = p.UnitPrice ?? 0,
                    });

            var result = await Task<IEnumerable<ProductPrice>>.Factory.FromAsync(query.BeginExecute(null, null), (ar) =>
            {
                return query.EndExecute(ar);
            });

            return new ProductReport<ProductPrice>(result);
        }

        /// <summary>
        /// Gets a product report with products, which price is more then products count.
        /// </summary>
        /// <param name="count">Items count.</param>
        /// <returns>Returns <see cref="ProductReport{ProductPrice}"/>.</returns>
        public async Task<ProductReport<ProductPrice>> GetPriceMoreThenProductsReport(int count)
        {
            var query = (DataServiceQuery<ProductPrice>)this.entities.Products.Where(p => p.UnitPrice.Value > count)
                .OrderByDescending(p => p.UnitPrice.Value)
                .Select(p => new ProductPrice()
                {
                    Name = p.ProductName,
                    Price = p.UnitPrice ?? 0,
                });

            var result = await Task<IEnumerable<ProductPrice>>.Factory.FromAsync(query.BeginExecute(null, null), (ar) =>
            {
                return query.EndExecute(ar);
            });

            return new ProductReport<ProductPrice>(result);
        }

        /// <summary>
        /// Gets a product report with products, which amount unit in stock is more then units on order.
        /// </summary>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public async Task<ProductReport<ProductPrice>> GetUnitsInStockProficit()
        {
            var query = (DataServiceQuery<ProductPrice>)(from p in this.entities.Products
                 where p.UnitsInStock.Value > p.UnitsOnOrder.Value
                 select new ProductPrice
                    {
                        Name = p.ProductName,
                        Price = p.UnitPrice ?? 0,
                    });

            var result = await Task<IEnumerable<ProductPrice>>.Factory.FromAsync(query.BeginExecute(null, null), (ar) =>
            {
                return query.EndExecute(ar);
            });

            return new ProductReport<ProductPrice>(result);
        }

        /// <summary>
        /// Gets a product report with products.
        /// </summary>
        /// <param name="countryCurrencyService">Country currency service.</param>
        /// <param name="currencyExchangeService">Currency exchange service.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public async Task<ProductReport<ProductLocalPrice>> GetCurrentProductsWithLocalCurrencyReport(ICountryCurrencyService countryCurrencyService, ICurrencyExchangeService currencyExchangeService)
        {
            var localPrices = (DataServiceQuery<ProductLocalPrice>)this.entities.Products.Select(p => new ProductLocalPrice
            {
                Name = p.ProductName,
                Price = p.UnitPrice ?? 0,
                Country = p.Supplier.Country,
            });

            var productsLocalPrices = await Task<IEnumerable<ProductLocalPrice>>.Factory.FromAsync(
                localPrices.BeginExecute(null, null),
                (ar) =>
                {
                    return localPrices.EndExecute(ar);
                });

            var listProducts = new List<ProductLocalPrice>();
            foreach (var product in productsLocalPrices)
            {
                var countryInfo = await countryCurrencyService?.GetLocalCurrencyByCountry(product.Country);
                var currencyExchange = await currencyExchangeService?.GetCurrencyExchangeRate("USD", countryInfo.CurrencyCode);
                product.CurrencySymbol = countryInfo.CurrencySymbol;
                product.LocalPrice = product.Price * currencyExchange;
                listProducts.Add(product);
            }

            return new ProductReport<ProductLocalPrice>(listProducts);
        }
    }
}
