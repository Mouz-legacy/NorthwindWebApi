// <copyright file="ProductCategoryPicturesManagementDataAccessService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Northwind.Services.DataAccess
{
    using System;
    using System.Data.SqlClient;
    using System.IO;
    using System.Threading.Tasks;
    using Northwind.DataAccess;
    using Northwind.DataAccess.Products;
    using Northwind.Services.Products;

    /// <summary>
    /// ProductCategoryPicturesManagementDataAccessService class.
    /// </summary>
    public class ProductCategoryPicturesManagementDataAccessService : IProductCategoryPicturesService
    {
        private readonly NorthwindDataAccessFactory northwindDataAccessFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductCategoryPicturesManagementDataAccessService"/> class.
        /// </summary>
        /// <param name="sqlConnection">Sql connection.</param>
        public ProductCategoryPicturesManagementDataAccessService(SqlConnection sqlConnection)
        {
            this.northwindDataAccessFactory = new SqlServerDataAccessFactory(sqlConnection) ?? throw new ArgumentNullException(nameof(sqlConnection));
        }

        /// <inheritdoc/>
        public async Task<bool> DestroyPictureAsync(int categoryId)
        {
            if (categoryId < 1)
            {
                throw new ArgumentException("CategoryId can't be less than one.", nameof(categoryId));
            }

            ProductCategoryTransferObject productCategoryTransferObject;
            try
            {
                productCategoryTransferObject = this.northwindDataAccessFactory.GetProductCategoryDataAccessObject().FindProductCategory(categoryId);
            }
            catch (ProductCategoryNotFoundException)
            {
                return false;
            }

            productCategoryTransferObject.Picture = null;
            if (await this.northwindDataAccessFactory.GetProductCategoryDataAccessObject().UpdateProductCategoryAsync(productCategoryTransferObject).ConfigureAwait(true))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <inheritdoc/>
        public bool TryShowPicture(int categoryId, out byte[] bytes)
        {
            if (categoryId < 1)
            {
                throw new ArgumentException("CategoryId can't be less than one.", nameof(categoryId));
            }

            ProductCategoryTransferObject productCategoryTransferObject;
            try
            {
                productCategoryTransferObject = this.northwindDataAccessFactory.GetProductCategoryDataAccessObject().FindProductCategory(categoryId);
            }
            catch (ProductCategoryNotFoundException)
            {
                bytes = null;
                return false;
            }

            bytes = productCategoryTransferObject.Picture;
            return true;
        }

        /// <inheritdoc/>
        public async Task<bool> UpdatePictureAsync(int categoryId, Stream stream)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (categoryId < 1)
            {
                throw new ArgumentException("CategoryId can't be less than one.", nameof(categoryId));
            }

            ProductCategoryTransferObject productCategoryTransferObject;

            try
            {
                productCategoryTransferObject = this.northwindDataAccessFactory.GetProductCategoryDataAccessObject().FindProductCategory(categoryId);
            }
            catch (ProductCategoryNotFoundException)
            {
                return false;
            }

            using var memoryStream = new MemoryStream();
            stream.Seek(0, SeekOrigin.Begin);
            stream.CopyTo(memoryStream);
            productCategoryTransferObject.Picture = memoryStream.ToArray();

            if (await this.northwindDataAccessFactory.GetProductCategoryDataAccessObject().UpdateProductCategoryAsync(productCategoryTransferObject).ConfigureAwait(true))
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
