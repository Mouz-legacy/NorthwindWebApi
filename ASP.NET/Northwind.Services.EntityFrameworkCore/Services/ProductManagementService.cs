// <copyright file="ProductManagementService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Nortwind.Services.EntityFrameworkCore.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Northwind.Services.EntityFrameworkCore.Context;
    using Northwind.Services.Products;

    /// <summary>
    /// Represents a stub for a product management service.
    /// </summary>
    public sealed class ProductManagementService : IProductManagementService
    {
        private readonly NorthwindContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductManagementService"/> class.
        /// </summary>
        /// <param name="context">Northwind context.</param>
        public ProductManagementService(NorthwindContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <inheritdoc/>
        public async Task<int> CreateProductAsync(Product product)
        {
            if (product is null)
            {
                return -1;
            }

            if (this.context.Products.Any())
            {
                product.ProductId = this.context.Products.Max(p => p.ProductId) + 1;
            }
            else
            {
                product.ProductId = 0;
            }

            this.context.Products.Add(product);
            await this.context.SaveChangesAsync().ConfigureAwait(true);
            return product.ProductId;
        }

        /// <inheritdoc/>
        public async Task<bool> DestroyProductAsync(int productId)
        {
            var product = await this.context.Products.FindAsync(productId).ConfigureAwait(true);
            if (product is not null)
            {
                this.context.Products.Remove(product);
                await this.context.SaveChangesAsync().ConfigureAwait(true);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <inheritdoc/>
        public Task<IList<Product>> LookupProductsByNameAsync(IList<string> names)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public async Task<IList<Product>> ShowProductsAsync(int offset, int limit)
        {
            return this.context.Products.Where(p => p.ProductId >= offset).Take(limit).ToList();
        }

        /// <inheritdoc/>
        public Task<IList<Product>> ShowProductsForCategoryAsync(int categoryId)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public bool TryShowProduct(int productId, out Product product)
        {
            product = this.context.Products.Find(productId);
            return product is not null;
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateProductAsync(int productId, Product product)
        {
            if (product is null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            var newProduct = this.context.Products.Single(c => c.ProductId == productId);
            if (newProduct is not null)
            {
                newProduct.ProductName = product.ProductName;
                newProduct.SupplierId = product.SupplierId;
                newProduct.CategoryId = product.CategoryId;
                newProduct.QuantityPerUnit = product.QuantityPerUnit;
                newProduct.UnitPrice = product.UnitPrice;
                newProduct.UnitsInStock = product.UnitsInStock;
                newProduct.UnitsOnOrder = product.UnitsOnOrder;
                newProduct.ReorderLevel = product.ReorderLevel;
                newProduct.Discontinued = product.Discontinued;
                await this.context.SaveChangesAsync().ConfigureAwait(true);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}