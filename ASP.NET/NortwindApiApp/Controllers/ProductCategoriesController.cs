// <copyright file="ProductCategoriesController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NorthwindWebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Northwind.Services.Products;

    /// <summary>
    /// ProductCategoriesController class.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class ProductCategoriesController : ControllerBase
    {
        private readonly IProductCategoryManagementService managementService;
        private readonly IProductCategoryPicturesService picturesService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductCategoriesController"/> class.
        /// </summary>
        /// <param name="managementService">Management service.</param>
        /// <param name="picturesService">Pictures service.</param>
        public ProductCategoriesController(IProductCategoryManagementService managementService, IProductCategoryPicturesService picturesService)
        {
            this.managementService = managementService ?? throw new ArgumentNullException(nameof(managementService));
            this.picturesService = picturesService ?? throw new ArgumentNullException(nameof(picturesService));
        }

        /// <summary>
        /// CreateCategory method.
        /// </summary>
        /// <param name="productCategory">ProductCategory entity.</param>
        /// <returns>Action result.</returns>
        [HttpPost]
        public async Task<ActionResult<Category>> CreateCategoryAsync(Category productCategory)
        {
            if (productCategory is null)
            {
                return this.BadRequest();
            }

            await this.managementService.CreateCategoryAsync(productCategory).ConfigureAwait(true);
            return this.Ok(productCategory);
        }

        /// <summary>
        /// GetCategory method.
        /// </summary>
        /// <param name="categoryId">Category id.</param>
        /// <returns>Action result.</returns>
        [HttpGet("{categoryId}")]
        public async Task<ActionResult<Category>> GetCategoryAsync(int categoryId)
        {
            if (this.managementService.TryShowCategory(categoryId, out Category productCategory))
            {
                return this.Ok(productCategory);
            }

            return this.NotFound();
        }

        /// <summary>
        /// GetCategories method.
        /// </summary>
        /// <param name="offset">Offset.</param>
        /// <param name="limit">Limit.</param>
        /// <returns>Action result.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategoriesAsync(int offset = 0, int limit = 10)
        {
            return this.Ok(await this.managementService.ShowCategoriesAsync(offset, limit).ConfigureAwait(true));
        }

        /// <summary>
        /// UpdateCategory method.
        /// </summary>
        /// <param name="categoryId">Category id.</param>
        /// <param name="productCategory">Product category entity.</param>
        /// <returns>Action result.</returns>
        [HttpPut("{categoryId}")]
        public async Task<ActionResult> UpdateCategoryAsync(int categoryId, Category productCategory)
        {
            if (categoryId != productCategory?.CategoryId)
            {
                return this.BadRequest();
            }

            await this.managementService.UpdateCategoriesAsync(categoryId, productCategory).ConfigureAwait(true);
            return this.NoContent();
        }

        /// <summary>
        /// DeleteCategory method.
        /// </summary>
        /// <param name="categoryId">Category id.</param>
        /// <returns>Action result.</returns>
        [HttpDelete("{categoryId}")]
        public async Task<ActionResult<Category>> DeleteCategoryAsync(int categoryId)
        {
            if (await this.managementService.DestroyCategoryAsync(categoryId).ConfigureAwait(true))
            {
                return this.NoContent();
            }
            else
            {
                return this.NotFound();
            }
        }

        /// <summary>
        /// PutPicture method.
        /// </summary>
        /// <param name="categoryId">Category id.</param>
        /// <param name="formFile">Form file.</param>
        /// <returns>Action result.</returns>
        [HttpPut("{categoryId}/picture")]
        public async Task<ActionResult> PutPictureAsync(int categoryId, IFormFile formFile)
        {
            if (categoryId < 1)
            {
                throw new ArgumentException("CategoryId can't be less than one.", nameof(categoryId));
            }

            await using var stream = new MemoryStream();
            await (formFile?.CopyToAsync(stream)).ConfigureAwait(true);
            if (!await this.picturesService.UpdatePictureAsync(categoryId, stream).ConfigureAwait(true))
            {
                return this.NotFound();
            }

            return this.NoContent();
        }

        /// <summary>
        /// GetPicture method.
        /// </summary>
        /// <param name="categoryId">Category id.</param>
        /// <returns>Action result.</returns>
        [HttpGet("{categoryId}/picture")]
        public async Task<ActionResult<byte[]>> GetPicture(int categoryId)
        {
            if (this.picturesService.TryShowPicture(categoryId, out byte[] picture))
            {
                return this.Ok(picture);
            }

            return this.NotFound();
        }

        /// <summary>
        /// DeletePicture method.
        /// </summary>
        /// <param name="categoryId">Category id.</param>
        /// <returns>Action result.</returns>
        [HttpDelete("{categoryId}/picture")]
        public async Task<ActionResult> DeletePictureAsync(int categoryId)
        {
            if (await this.picturesService.DestroyPictureAsync(categoryId).ConfigureAwait(true))
            {
                return this.NoContent();
            }

            return this.NotFound();
        }
    }
}
