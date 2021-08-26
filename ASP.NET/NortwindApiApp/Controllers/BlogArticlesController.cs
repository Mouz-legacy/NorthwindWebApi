// <copyright file="BlogArticlesController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NortwindApiApp.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Northwind.Services.Blogging;
    using Northwind.Services.Employees;
    using Northwind.Services.Products;
    using NortwindApiApp.Models;

    /// <summary>
    /// BlogArticlesController class.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class BlogArticlesController : Controller
    {
        private readonly IBloggingService bloggingService;

        /// <summary>
        /// Initializes a new instance of the <see cref="BlogArticlesController"/> class.
        /// </summary>
        /// <param name="bloggingService">Input service.</param>
        public BlogArticlesController(IBloggingService bloggingService)
        {
            this.bloggingService = bloggingService ?? throw new ArgumentNullException(nameof(bloggingService), "Input service was null!");
        }

        /// <summary>
        /// CreateArticleAsync method.
        /// </summary>
        /// <param name="blogArticle">Converted blog article model.</param>
        /// <param name="employeeService">Employee service.</param>
        /// <returns>Action result.</returns>
        [HttpPost]
        public async Task<ActionResult<int>> CreateArticleAsync(ArticleCreateModel blogArticle, [FromServices] IEmployeeManagementService employeeService)
        {
            if (blogArticle is null || employeeService is null || employeeService.TryShowEmployee(blogArticle.EmployeeId, out _))
            {
                return this.BadRequest();
            }

            return await this.bloggingService.CreateArticleAsync(blogArticle).ConfigureAwait(true);
        }

        /// <summary>
        /// DeleteArticleAsync method.
        /// </summary>
        /// <param name="blogArticleId">If of blog article instance.</param>
        /// <returns>Action result.</returns>
        [HttpDelete("{blogArticleId}")]
        public async Task<ActionResult> DeleteArticleAsync(int blogArticleId)
        {
            if (await this.bloggingService.DestroyArticleAsync(blogArticleId).ConfigureAwait(true))
            {
                return this.NoContent();
            }
            else
            {
                return this.NotFound();
            }
        }

        /// <summary>
        /// GetArticlesAsync method.
        /// </summary>
        /// <param name="employeeService">Employee service.</param>
        /// <param name="offset">Offset of articles.</param>
        /// <param name="limit">Max amount of articles.</param>
        /// <returns>Action result.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BlogArticle>>> GetArticlesAsync([FromServices] IEmployeeManagementService employeeService, int offset = 0, int limit = 10)
        {
            if (offset < 0 || limit <= 0 || employeeService is null)
            {
                return this.BadRequest();
            }

            var articles = await this.bloggingService.ShowArticlesAsync(offset, limit).ConfigureAwait(true);
            var articleResponseList = new List<ArticleGetModel>();
            foreach (var blogArticle in articles)
            {
                if (employeeService.TryShowEmployee(blogArticle.EmployeeId, out Employee employee))
                {
                    var articleResponse = new ArticleGetModel(blogArticle, employee);
                    articleResponseList.Add(articleResponse);
                }
            }

            return this.Ok(articleResponseList);
        }

        /// <summary>
        /// GetArticle method.
        /// </summary>
        /// <param name="blogArticleId">Blog article id.</param>
        /// <param name="employeeService">Employee service.</param>
        /// <returns>Action result.</returns>
        [HttpGet("{blogArticleId}")]
        public async Task<ActionResult<BlogArticle>> GetArticle(int blogArticleId, [FromServices] IEmployeeManagementService employeeService)
        {
            if (blogArticleId <= 0 || employeeService is null)
            {
                return this.BadRequest();
            }

            if (this.bloggingService.TryShowArticle(blogArticleId, out BlogArticle article))
            {
                employeeService.TryShowEmployee(article.EmployeeId, out Employee employee);
                return this.Ok(new ArticleGetSingleModel(article, employee));
            }
            else
            {
                return this.BadRequest();
            }
        }

        /// <summary>
        /// UpdateArticleAsync method.
        /// </summary>
        /// <param name="blogArticleId">Blog article id.</param>
        /// <param name="blogArticleUpdateQuery">Article converting model.</param>
        /// <returns>Action result.</returns>
        [HttpPut("{blogArticleId}")]
        public async Task<ActionResult> UpdateArticleAsync(int blogArticleId, ArticleUpdateModel blogArticleUpdateQuery)
        {
            if (blogArticleId <= 0)
            {
                return this.BadRequest();
            }

            await this.bloggingService.UpdateArticleAsync(blogArticleId, blogArticleUpdateQuery).ConfigureAwait(true);
            return this.NoContent();
        }

        /// <summary>
        /// CreateProductLinkForArticleAsync method.
        /// </summary>
        /// <param name="blogArticleId">Article id.</param>
        /// <param name="productId">Product id.</param>
        /// <param name="productManagementService">Product service.</param>
        /// <returns>Action result.</returns>
        [HttpPost("{blogArticleId}/product/{productId}")]
        public async Task<ActionResult<int>> CreateProductLinkForArticleAsync(int blogArticleId, int productId, [FromServices] IProductManagementService productManagementService)
        {
            if (blogArticleId <= 0 || productId <= 0 || productManagementService is null || !(this.bloggingService.TryShowArticle(blogArticleId, out _) && productManagementService.TryShowProduct(productId, out _)))
            {
                return this.BadRequest();
            }

            return await this.bloggingService.CreateProductLinkForArticleAsync(blogArticleId, productId).ConfigureAwait(true);
        }

        /// <summary>
        /// GetProductsForArticleAsync method.
        /// </summary>
        /// <param name="blogArticleId">Article id.</param>
        /// <returns>Action result.</returns>
        [HttpGet("{blogArticleId}/product")]
        public async Task<ActionResult<IEnumerable<BlogArticleProduct>>> GetProductsForArticleAsync(int blogArticleId)
        {
            if (blogArticleId <= 0 || !this.bloggingService.TryShowArticle(blogArticleId, out _))
            {
                return this.BadRequest();
            }

            return this.Ok(await this.bloggingService.GetProductsForArticleAsync(blogArticleId).ConfigureAwait(true));
        }

        /// <summary>
        /// DestroyProductLinkForArticleAsync method.
        /// </summary>
        /// <param name="blogArticleId">Blog article id.</param>
        /// <param name="productId">Product id.</param>
        /// <returns>Action result.</returns>
        [HttpDelete("{blogArticleId}/product/{productId}")]
        public async Task<ActionResult> DestroyProductLinkForArticleAsync(int blogArticleId, int productId)
        {
            if (blogArticleId <= 0 || productId <= 0)
            {
                return this.BadRequest();
            }

            var articleProduct = (await this.bloggingService.GetProductsForArticleAsync(blogArticleId).ConfigureAwait(true)).Single(ap => ap.ProductId == productId);
            if (await this.bloggingService.DestroyProductLinkForArticleAsync(articleProduct.BlogArticleProductId).ConfigureAwait(true))
            {
                return this.NoContent();
            }
            else
            {
                return this.NotFound();
            }
        }

        /// <summary>
        /// CreateBlogCommentAsync method.
        /// </summary>
        /// <param name="blogArticleId">Blog article id.</param>
        /// <param name="blogComment">Blog comment id.</param>
        /// <returns>Action result.</returns>
        [HttpPost("{blogArticleId}/comments")]
        public async Task<ActionResult<int>> CreateBlogCommentAsync(int blogArticleId, BlogComment blogComment)
        {
            if (blogArticleId <= 0 || blogComment is null || blogComment.Text is null)
            {
                return this.BadRequest();
            }

            blogComment.ArticleId = blogArticleId;
            return await this.bloggingService.CreateBlogCommentAsync(blogComment).ConfigureAwait(true);
        }

        /// <summary>
        /// DeleteBlogCommentAsync method.
        /// </summary>
        /// <param name="blogCommentId">blog comment id.</param>
        /// <returns>Action result.</returns>
        [HttpDelete("{blogArticleId}/comments/{blogCommentId}")]
        public async Task<ActionResult> DeleteBlogCommentAsync(int blogCommentId)
        {
            if (blogCommentId <= 0)
            {
                return this.BadRequest();
            }
            else if (await this.bloggingService.DestroyBlogCommentAsync(blogCommentId).ConfigureAwait(true))
            {
                return this.NoContent();
            }
            else
            {
                return this.NotFound();
            }
        }

        /// <summary>
        /// GetCommentsForArticles method.
        /// </summary>
        /// <param name="blogArticleId">Blog article id.</param>
        /// <returns>Action result.</returns>
        [HttpGet("{blogArticleId}/comments")]
        public async Task<ActionResult<IEnumerable<BlogComment>>> GetCommentsForArticles(int blogArticleId)
        {
            if (blogArticleId <= 0)
            {
                return this.BadRequest();
            }

            return this.Ok(await this.bloggingService.GetCommentsForArticleAsync(blogArticleId).ConfigureAwait(true));
        }

        /// <summary>
        /// UpdateBlogCommentAsync method.
        /// </summary>
        /// <param name="blogArticleId">Blog article id.</param>
        /// <param name="blogCommentId">Blog comment id.</param>
        /// <param name="blogComment">Blog comment instance.</param>
        /// <returns>Action result.</returns>
        [HttpPut("{blogArticleId}/comments/{blogCommentId}")]
        public async Task<ActionResult> UpdateBlogCommentAsync(int blogArticleId, int blogCommentId, BlogComment blogComment)
        {
            if (blogArticleId <= 0 || blogCommentId <= 0 || blogComment is null)
            {
                return this.BadRequest();
            }

            await this.bloggingService.UpdateBlogCommentAsync(blogArticleId, blogCommentId, blogComment).ConfigureAwait(true);
            return this.NoContent();
        }
    }
}
