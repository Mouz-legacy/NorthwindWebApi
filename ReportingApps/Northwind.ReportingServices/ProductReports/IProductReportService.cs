// <copyright file="IProductReportService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Northwind.ReportingSerivces.ProductReports
{
    using System.Threading.Tasks;
    using Northwind.CurrencyServices.CountryCurrency;
    using Northwind.CurrencyServices.CurrencyExchange;
    using Northwind.ReportingServices.ProductReports;

    /// <summary>
    /// IProductReportService interface.
    /// </summary>
    public interface IProductReportService
    {
        /// <summary>
        /// Gets a product report with all current products.
        /// </summary>
        /// <returns>Returns <see cref="ProductReport{T}"/>.</returns>
        public Task<ProductReport<ProductPrice>> GetCurrentProductsReport();

        /// <summary>
        /// Gets a product report with most expensive products.
        /// </summary>
        /// <param name="count">Items count.</param>
        /// <returns>Returns <see cref="ProductReport{ProductPrice}"/>.</returns>
        public Task<ProductReport<ProductPrice>> GetMostExpensiveProductsReport(int count);

        /// <summary>
        /// Gets a product report with products, which price is less then products count.
        /// </summary>
        /// <param name="count">Items count.</param>
        /// <returns>Returns <see cref="ProductReport{ProductPrice}"/>.</returns>
        public Task<ProductReport<ProductPrice>> GetPriceLessThenProductsReport(int count);

        /// <summary>
        /// Gets a product report with products, which price is between left and right operands.
        /// </summary>
        /// <param name="leftSide">Left side operand.</param>
        /// <param name="rightSide">Right side operand.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public Task<ProductReport<ProductPrice>> GetPriceBetweenProductsReport(int leftSide, int rightSide);

        /// <summary>
        /// Gets a product report with products, which price is above average.
        /// </summary>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public Task<ProductReport<ProductPrice>> GetPriceAboveAverageProductsReport();

        /// <summary>
        /// Gets a product report with products, which amount unit in stock is less then units on order.
        /// </summary>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public Task<ProductReport<ProductPrice>> GetUnitsInStockDeficit();

        /// <summary>
        /// Gets a product report with products, which price is more then products count.
        /// </summary>
        /// <param name="count">Items count.</param>
        /// <returns>Returns <see cref="ProductReport{ProductPrice}"/>.</returns>
        public Task<ProductReport<ProductPrice>> GetPriceMoreThenProductsReport(int count);

        /// <summary>
        /// Gets a product report with products, which amount unit in stock is more then units on order.
        /// </summary>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public Task<ProductReport<ProductPrice>> GetUnitsInStockProficit();

        /// <summary>
        /// Gets a product report with products.
        /// </summary>
        /// <param name="countryCurrencyService">Country currency service.</param>
        /// <param name="currencyExchangeService">Currency exchange service.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public Task<ProductReport<ProductLocalPrice>> GetCurrentProductsWithLocalCurrencyReport(ICountryCurrencyService countryCurrencyService, ICurrencyExchangeService currencyExchangeService);
    }
}
