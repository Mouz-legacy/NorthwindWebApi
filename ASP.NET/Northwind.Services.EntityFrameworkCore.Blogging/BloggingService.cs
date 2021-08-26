// <copyright file="BloggingService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Northwind.Services.EntityFrameworkCore.Blogging
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Northwind.Services.Blogging;
    using Northwind.Services.EntityFrameworkCore.Blogging.Context;

    /// <summary>
    /// BloggingService class.
    /// </summary>
    public class BloggingService : IBloggingService
    {
        private readonly BloggingContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="BloggingService"/> class.
        /// </summary>
        /// <param name="context">Blogging context.</param>
        public BloggingService(BloggingContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context), "Input article was null!");
        }

        /// <summary>
        /// CreateArticleAsync method to create new blog article.
        /// </summary>
        /// <param name="article">New blog article.</param>
        /// <returns>Id of created article.</returns>
        public async Task<int> CreateArticleAsync(BlogArticle article)
        {
            if (article is null)
            {
                throw new ArgumentNullException(nameof(article));
            }

            if (this.context.Articles.Any())
            {
                article.BlogArticleId = this.context.Articles.Max(m => m.BlogArticleId) + 1;
            }
            else
            {
                article.BlogArticleId = 0;
            }

            await this.context.AddAsync(article).ConfigureAwait(true);
            await this.context.SaveChangesAsync().ConfigureAwait(true);
            return article.BlogArticleId;
        }

        /// <summary>
        /// DestroyArticleAsync method to destroy blog article by input id.
        /// </summary>
        /// <param name="articleId">Blog article id.</param>
        /// <returns>True, if article was deleted. False otherwise.</returns>
        public async Task<bool> DestroyArticleAsync(int articleId)
        {
            var article = await this.context.Articles.FindAsync(articleId).ConfigureAwait(true);
            if (article is not null)
            {
                this.context.Articles.Remove(article);
                await this.context.SaveChangesAsync().ConfigureAwait(true);

                return true;
            }

            return false;
        }

        /// <summary>
        /// ShowArticleAsync method to show blog articles.
        /// </summary>
        /// <param name="offset">Offset of articles.</param>
        /// <param name="limit">Amount of articles.</param>
        /// <returns>IList of blog articles.</returns>
        public async Task<IList<BlogArticle>> ShowArticlesAsync(int offset, int limit)
        {
            return this.context.Articles.Where(a => a.BlogArticleId >= offset).Take(limit).ToList();
        }

        /// <summary>
        /// TryShowArticle method to try to show article.
        /// </summary>
        /// <param name="articleId">Blog article id.</param>
        /// <param name="article">Blog article.</param>
        /// <returns>True, if article exist. False otherwise.</returns>
        public bool TryShowArticle(int articleId, out BlogArticle article)
        {
            article = this.context.Articles.Find(articleId);
            return article is not null;
        }

        /// <summary>
        /// UpdateArticleAsync to show blog articles.
        /// </summary>
        /// <param name="articleId">Blog article id.</param>
        /// <param name="article">Blog article instance.</param>
        /// <returns>True, if can update.</returns>
        public async Task<bool> UpdateArticleAsync(int articleId, BlogArticle article)
        {
            if (article is null)
            {
                throw new ArgumentNullException(nameof(article), "Input article was null!");
            }

            var blogArticle = this.context.Articles.Single(a => a.BlogArticleId == articleId);
            if (blogArticle is not null)
            {
                article.Title = blogArticle.Title;
                article.Body = blogArticle.Body;
                article.PublicationDate = blogArticle.PublicationDate;
                await this.context.SaveChangesAsync().ConfigureAwait(true);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// CreateProductLinkForArticleAsync method.
        /// </summary>
        /// <param name="blogArticleId">Blog article id.</param>
        /// <param name="productId">Product id.</param>
        /// <returns>Id.</returns>
        public async Task<int> CreateProductLinkForArticleAsync(int blogArticleId, int productId)
        {
            if (blogArticleId <= 0)
            {
                throw new ArgumentNullException(nameof(blogArticleId));
            }
            else if (productId <= 0)
            {
                throw new ArgumentNullException(nameof(productId));
            }

            var linkToProduct = (await this.GetProductsForArticleAsync(blogArticleId).ConfigureAwait(true)).Any(p => p.ArticleId == blogArticleId && p.ProductId == productId);
            if (!linkToProduct)
            {
                var articleProduct = new BlogArticleProduct(blogArticleId, productId);
                await this.context.ArticleProducts.AddAsync(articleProduct).ConfigureAwait(true);
                await this.context.SaveChangesAsync().ConfigureAwait(true);

                return articleProduct.BlogArticleProductId;
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// DestroyProductLinkForArticleAsync method.
        /// </summary>
        /// <param name="blogArticleProductId">Article product id.</param>
        /// <returns>True, if were destroyed.</returns>
        public async Task<bool> DestroyProductLinkForArticleAsync(int blogArticleProductId)
        {
            var articleProducts = await this.context.ArticleProducts.FindAsync(blogArticleProductId).ConfigureAwait(true);
            if (articleProducts is not null)
            {
                this.context.ArticleProducts.Remove(articleProducts);
                await this.context.SaveChangesAsync().ConfigureAwait(true);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// GetProductsForArticleAsync method.
        /// </summary>
        /// <param name="blogArticleId">Blog article id.</param>
        /// <returns>List of articles.</returns>
        public async Task<IList<BlogArticleProduct>> GetProductsForArticleAsync(int blogArticleId)
        {
            if (blogArticleId <= 0)
            {
                throw new ArgumentNullException(nameof(blogArticleId));
            }

            return await this.context.ArticleProducts.Where(p => p.ArticleId == blogArticleId).ToListAsync().ConfigureAwait(true);
        }

        /// <summary>
        /// CreateBlogCommentAsync method.
        /// </summary>
        /// <param name="blogComment">Blog comment instance.</param>
        /// <returns>Blog comment id.</returns>
        public async Task<int> CreateBlogCommentAsync(BlogComment blogComment)
        {
            if (blogComment is null)
            {
                throw new ArgumentNullException(nameof(blogComment));
            }

            if (this.context.Articles.Any(a => a.BlogArticleId == blogComment.ArticleId))
            {
                blogComment.BlogCommentId = 0;
                await this.context.Comments.AddAsync(blogComment).ConfigureAwait(true);
                await this.context.SaveChangesAsync().ConfigureAwait(true);
                return blogComment.BlogCommentId;
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// DestroyBlogCommentAsync method.
        /// </summary>
        /// <param name="blogCommentId">Blog comment id.</param>
        /// <returns>True, if comment was deleted. False otherwise.</returns>
        public async Task<bool> DestroyBlogCommentAsync(int blogCommentId)
        {
            var blogComment = await this.context.Comments.FindAsync(blogCommentId).ConfigureAwait(true);
            if (blogComment is not null)
            {
                this.context.Comments.Remove(blogComment);
                await this.context.SaveChangesAsync().ConfigureAwait(true);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// GetCommentsForArticleAsync method.
        /// </summary>
        /// <param name="blogArticleId">Blog article id.</param>
        /// <returns>List of blog comments.</returns>
        public async Task<IList<BlogComment>> GetCommentsForArticleAsync(int blogArticleId)
        {
            if (blogArticleId <= 0)
            {
                throw new ArgumentNullException(nameof(blogArticleId));
            }

            return await this.context.Comments.Where(c => c.ArticleId == blogArticleId).ToListAsync().ConfigureAwait(true);
        }

        /// <summary>
        /// UpdateBlogCommentAsync method.
        /// </summary>
        /// <param name="blogArticleId">Blog article id.</param>
        /// <param name="blogCommentId">Blog comment id.</param>
        /// <param name="blogComment">Blog comment instance.</param>
        /// <returns>True, if comment was updated. False otherwise.</returns>
        public async Task<bool> UpdateBlogCommentAsync(int blogArticleId, int blogCommentId, BlogComment blogComment)
        {
            if (blogComment is null)
            {
                throw new ArgumentNullException(nameof(blogComment));
            }

            var comment = this.context.Comments.Single(c => c.ArticleId == blogArticleId && c.BlogCommentId == blogCommentId);
            if (comment is not null)
            {
                comment.Text = blogComment.Text;
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
