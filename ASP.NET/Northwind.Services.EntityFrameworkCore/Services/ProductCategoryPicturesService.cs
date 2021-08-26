// <copyright file="ProductCategoryPicturesService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Nortwind.Services.EntityFrameworkCore.Services
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Northwind.Services.EntityFrameworkCore.Context;
    using Northwind.Services.Products;

    /// <summary>
    /// Represents a product category management service.
    /// </summary>
    public class ProductCategoryPicturesService : IProductCategoryPicturesService
    {
        private readonly NorthwindContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductCategoryPicturesService"/> class.
        /// </summary>
        /// <param name="context">Northwind context.</param>
        public ProductCategoryPicturesService(NorthwindContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <inheritdoc/>
        public bool TryShowPicture(int categoryId, out byte[] bytes)
        {
            if (categoryId < 1)
            {
                throw new ArgumentException("CategoryId can't be less than one.", nameof(categoryId));
            }

            var category = this.context.Categories.Find(categoryId);
            if (category is null)
            {
                bytes = null;
                return false;
            }

            bytes = category.Picture;
            return true;
        }

        /// <inheritdoc/>
        public async Task<bool> UpdatePictureAsync(int categoryId, Stream stream)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            var category = await this.context.Categories.FindAsync(categoryId).ConfigureAwait(true);
            if (category is null)
            {
                return false;
            }

            await using var memoryStream = new MemoryStream();
            stream.Seek(0, SeekOrigin.Begin);
            await stream.CopyToAsync(memoryStream).ConfigureAwait(true);
            category.Picture = memoryStream.ToArray();
            this.context.Update(category);
            await this.context.SaveChangesAsync().ConfigureAwait(true);
            return true;
        }

        /// <inheritdoc/>
        public async Task<bool> DestroyPictureAsync(int categoryId)
        {
            if (categoryId < 1)
            {
                throw new ArgumentException("CategoryId can't be less than one.", nameof(categoryId));
            }

            var category = await this.context.Categories.FindAsync(categoryId).ConfigureAwait(true);
            if (category is null)
            {
                return false;
            }

            category.Picture = null;
            this.context.Update(category);
            await this.context.SaveChangesAsync().ConfigureAwait(true);
            return true;
        }
    }
}
