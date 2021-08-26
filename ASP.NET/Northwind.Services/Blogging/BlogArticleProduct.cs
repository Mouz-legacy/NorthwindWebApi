// <copyright file="BlogArticleProduct.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Northwind.Services.Blogging
{
    /// <summary>
    /// BlogArticleProduct class.
    /// </summary>
    public class BlogArticleProduct
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BlogArticleProduct"/> class.
        /// </summary>
        public BlogArticleProduct()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BlogArticleProduct"/> class.
        /// </summary>
        /// <param name="blogArticleId">A blog article identifier.</param>
        /// <param name="productId">A product identifier.</param>
        public BlogArticleProduct(int blogArticleId, int productId)
        {
            this.BlogArticleProductId = 0;
            this.ArticleId = blogArticleId;
            this.ProductId = productId;
        }

        /// <summary>
        /// Gets or sets BlogArticleProductId.
        /// </summary>
        public int BlogArticleProductId { get; set; }

        /// <summary>
        /// Gets or sets articles id.
        /// </summary>
        public int ArticleId { get; set; }

        /// <summary>
        /// Gets or sets article product id.
        /// </summary>
        public int ProductId { get; set; }
    }
}
