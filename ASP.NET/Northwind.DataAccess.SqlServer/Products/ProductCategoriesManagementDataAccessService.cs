// <copyright file="ProductCategoriesManagementDataAccessService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Northwind.Services.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Threading.Tasks;
    using Northwind.DataAccess;
    using Northwind.DataAccess.Products;
    using Northwind.Services.Products;

    /// <summary>
    /// ProductCategoriesManagementDataAccessService class.
    /// </summary>
    public class ProductCategoriesManagementDataAccessService : IProductCategoryManagementService
    {
        private readonly NorthwindDataAccessFactory northwindDataAccessFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductCategoriesManagementDataAccessService"/> class.
        /// </summary>
        /// <param name="sqlConnection">Sql connection.</param>
        public ProductCategoriesManagementDataAccessService(SqlConnection sqlConnection)
        {
            this.northwindDataAccessFactory = new SqlServerDataAccessFactory(sqlConnection) ?? throw new ArgumentNullException(nameof(sqlConnection));
        }

        /// <inheritdoc/>
        public async Task<int> CreateCategoryAsync(Category productCategory)
        {
            if (productCategory is null)
            {
                throw new ArgumentNullException(nameof(productCategory));
            }

            return await this.northwindDataAccessFactory.GetProductCategoryDataAccessObject().InsertProductCategoryAsync((ProductCategoryTransferObject)productCategory).ConfigureAwait(true);
        }

        /// <inheritdoc/>
        public async Task<bool> DestroyCategoryAsync(int categoryId)
        {
            if (categoryId < 1)
            {
                throw new ArgumentException("CategoryId can't be less than one.", nameof(categoryId));
            }

            return await this.northwindDataAccessFactory.GetProductCategoryDataAccessObject().DeleteProductCategoryAsync(categoryId).ConfigureAwait(true);
        }

        /// <inheritdoc/>
        public Task<IList<Category>> LookupCategoriesByNameAsync(IList<string> names)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public async Task<IList<Category>> ShowCategoriesAsync(int offset, int limit)
        {
            var productCategories = new List<Category>();
            foreach (var productTransferObject in await this.northwindDataAccessFactory.GetProductCategoryDataAccessObject().SelectProductCategoriesAsync(offset, limit).ConfigureAwait(true))
            {
                productCategories.Add((Category)productTransferObject);
            }

            return productCategories;
        }

        /// <inheritdoc/>
        public bool TryShowCategory(int categoryId, out Category productCategory)
        {
            if (categoryId < 1)
            {
                throw new ArgumentException("CategoryId can't be less than one.", nameof(categoryId));
            }

            try
            {
                productCategory = (Category)this.northwindDataAccessFactory.GetProductCategoryDataAccessObject().FindProductCategory(categoryId);
            }
            catch (ProductNotFoundException)
            {
                productCategory = null;
                return false;
            }

            return true;
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateCategoriesAsync(int categoryId, Category productCategory)
        {
            if (productCategory is null)
            {
                throw new ArgumentNullException(nameof(productCategory));
            }

            if (categoryId != productCategory.CategoryId)
            {
                return false;
            }

            if (await this.northwindDataAccessFactory.GetProductCategoryDataAccessObject().UpdateProductCategoryAsync((ProductCategoryTransferObject)productCategory).ConfigureAwait(true))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
