// <copyright file="IBloggingService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Northwind.Services.Blogging
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// IBloggingService interface that describe base method to work with BlogArticle.
    /// </summary>
    public interface IBloggingService
    {
        /// <summary>
        /// ShowArticleAsync method to show article.
        /// </summary>
        /// <param name="offset">Offset of articles.</param>
        /// <param name="limit">Limit of articles.</param>
        /// <returns>Task of IList BlogArticle.</returns>
        Task<IList<BlogArticle>> ShowArticlesAsync(int offset, int limit);

        /// <summary>
        /// TryShowArticle to try show an article.
        /// </summary>
        /// <param name="articleId">Id of blog article.</param>
        /// <param name="article">Blog article.</param>
        /// <returns>True, if can show this article. False otherwise.</returns>
        bool TryShowArticle(int articleId, out BlogArticle article);

        /// <summary>
        /// CreateArticleAsync method to create new blog article.
        /// </summary>
        /// <param name="article">New blog article to create.</param>
        /// <returns>Id of new article.</returns>
        Task<int> CreateArticleAsync(BlogArticle article);

        /// <summary>
        /// DestroyArticleAsync to destroy article.
        /// </summary>
        /// <param name="articleId">Article id to delete.</param>
        /// <returns>True, if article was deleted. False otherwise.</returns>
        Task<bool> DestroyArticleAsync(int articleId);

        /// <summary>
        /// UpdateArticleAsync method to update article.
        /// </summary>
        /// <param name="articleId">Article id.</param>
        /// <param name="article">Article.</param>
        /// <returns>True, if article was updated. False otherwise.</returns>
        Task<bool> UpdateArticleAsync(int articleId, BlogArticle article);

        /// <summary>
        /// Creates a link to a product for a blog article.
        /// </summary>
        /// <param name="blogArticleId">A blog article identifier.</param>
        /// <param name="productId">A product identifier.</param>
        /// <returns>An identifier of a created link.</returns>
        Task<int> CreateProductLinkForArticleAsync(int blogArticleId, int productId);

        /// <summary>
        /// Returns all related products for a blog article.
        /// </summary>
        /// <param name="blogArticleId">A blog article identifier.</param>
        /// <returns>All related products.</returns>
        Task<IList<BlogArticleProduct>> GetProductsForArticleAsync(int blogArticleId);

        /// <summary>
        /// Destroys an exited link to a product for a blog article.
        /// </summary>
        /// <param name="blogArticleProductId">A blog article product identifier.</param>
        /// <returns>
        /// True if a link to product for a blog article is destroyed; otherwise false.
        /// </returns>
        Task<bool> DestroyProductLinkForArticleAsync(int blogArticleProductId);

        /// <summary>
        /// Creates a new blog comment.
        /// </summary>
        /// <param name="blogComment">A <see cref="BlogComment"/> to create.</param>
        /// <returns>An identifier of a created blog comment.</returns>
        Task<int> CreateBlogCommentAsync(BlogComment blogComment);

        /// <summary>
        /// Destroys an exited blog comment.
        /// </summary>
        /// <param name="blogCommentId">A blog comment identifier.</param>
        /// <returns>True if a blog comment is destroyed; otherwise false.</returns>
        Task<bool> DestroyBlogCommentAsync(int blogCommentId);

        /// <summary>
        /// Returns all related comments for a blog article.
        /// </summary>
        /// <param name="blogArticleId">A blog article identifier.</param>
        /// <returns>All related comments.</returns>
        Task<IList<BlogComment>> GetCommentsForArticleAsync(int blogArticleId);

        /// <summary>
        /// Updates a blog comment async.
        /// </summary>
        /// <param name="blogArticleId">A blog article id.</param>
        /// <param name="blogCommentId">A blog comment id.</param>
        /// <param name="blogComment">A <see cref="BlogComment"/> to update.</param>
        /// <returns>True, if a blog comment is updated. Otherwise false.</returns>
        Task<bool> UpdateBlogCommentAsync(int blogArticleId, int blogCommentId, BlogComment blogComment);
    }
}
