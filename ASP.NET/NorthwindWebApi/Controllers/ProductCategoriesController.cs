// <copyright file="ProductCategoriesController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NorthwindWebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Nortwind.Services.Products;

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
        public ActionResult<ProductCategory> CreateCategory(ProductCategory productCategory)
        {
            if (productCategory is null)
            {
                return this.BadRequest();
            }

            this.managementService.CreateCategory(productCategory);
            return this.Ok(productCategory);
        }

        /// <summary>
        /// GetCategory method.
        /// </summary>
        /// <param name="categoryId">Category id.</param>
        /// <returns>Action result.</returns>
        [HttpGet("{categoryId}")]
        public ActionResult<ProductCategory> GetCategory(int categoryId)
        {
            if (this.managementService.TryShowCategory(categoryId, out ProductCategory productCategory))
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
        public ActionResult<IEnumerable<ProductCategory>> GetCategories(int offset = 0, int limit = 10)
        {
            return this.Ok(this.managementService.ShowCategories(offset, limit));
        }

        /// <summary>
        /// UpdateCategory method.
        /// </summary>
        /// <param name="categoryId">Category id.</param>
        /// <param name="productCategory">Product category entity.</param>
        /// <returns>Action result.</returns>
        [HttpPut("{categoryId}")]
        public ActionResult UpdateCategory(int categoryId, ProductCategory productCategory)
        {
            if (categoryId != productCategory?.Id)
            {
                return this.BadRequest();
            }

            this.managementService.UpdateCategories(categoryId, productCategory);
            return this.NoContent();
        }

        /// <summary>
        /// DeleteCategory method.
        /// </summary>
        /// <param name="categoryId">Category id.</param>
        /// <returns>Action result.</returns>
        [HttpDelete("{categoryId}")]
        public ActionResult<ProductCategory> DeleteCategory(int categoryId)
        {
            if (this.managementService.DestroyCategory(categoryId))
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
        public ActionResult PutPicture(int categoryId, IFormFile formFile)
        {
            if (categoryId < 1)
            {
                throw new ArgumentException("CategoryId can't be less than one.", nameof(categoryId));
            }

            using var stream = new MemoryStream();
            formFile?.CopyTo(stream);
            if (!this.picturesService.UpdatePicture(categoryId, stream))
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
        public ActionResult<byte[]> GetPicture(int categoryId)
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
        public ActionResult DeletePicture(int categoryId)
        {
            if (this.picturesService.DestroyPicture(categoryId))
            {
                return this.NoContent();
            }

            return this.NotFound();
        }
    }
}
