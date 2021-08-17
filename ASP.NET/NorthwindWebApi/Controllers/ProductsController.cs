// <copyright file="ProductsController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NorthwindWebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Northwind.Services.Products;

    /// <summary>
    /// ProductsController class.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class ProductsController : Controller
    {
        private readonly IProductManagementService productManagementService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductsController"/> class.
        /// </summary>
        /// <param name="productManagementService">service.</param>
        public ProductsController(IProductManagementService productManagementService)
        {
            this.productManagementService = productManagementService ?? throw new ArgumentNullException(nameof(productManagementService));
        }

        /// <summary>
        /// CreateProduct method.
        /// </summary>
        /// <param name="product">Product.</param>
        /// <returns>Action result.</returns>
        [HttpPost]
        public ActionResult<Product> CreateProduct(Product product)
        {
            if (product is null)
            {
                return this.BadRequest();
            }

            this.productManagementService.CreateProduct(product);
            return this.Ok(product);
        }

        /// <summary>
        /// GetProducts method.
        /// </summary>
        /// <param name="offset">Offset.</param>
        /// <param name="limit">Limit data.</param>
        /// <returns>Action result.</returns>
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts(int offset = 0, int limit = 10)
        {
            if (offset >= 0 && limit > 0)
            {
                return this.Ok(this.productManagementService.ShowProducts(offset, limit));
            }
            else
            {
                return this.BadRequest();
            }
        }

        /// <summary>
        /// GetProduct method.
        /// </summary>
        /// <param name="productId">Product id.</param>
        /// <returns>Action result.</returns>
        [HttpGet("{productId}")]
        public ActionResult<Product> GetProduct(int productId)
        {
            if (this.productManagementService.TryShowProduct(productId, out Product product))
            {
                return this.Ok(product);
            }
            else
            {
                return this.BadRequest();
            }
        }

        /// <summary>
        /// UpdateProduct method.
        /// </summary>
        /// <param name="productId">Product id.</param>
        /// <param name="product">Product.</param>
        /// <returns>Action result.</returns>
        [HttpPut("{productId}")]
        public ActionResult UpdateProduct(int productId, Product product)
        {
            if (productId != product?.Id)
            {
                return this.BadRequest();
            }

            this.productManagementService.UpdateProduct(productId, product);
            return this.NoContent();
        }

        /// <summary>
        /// DeleteProduct method.
        /// </summary>
        /// <param name="productId">Product id.</param>
        /// <returns>Action result.</returns>
        [HttpDelete("{productId}")]
        public ActionResult<Product> DeleteProduct(int productId)
        {
            if (this.productManagementService.DestroyProduct(productId))
            {
                return this.NoContent();
            }
            else
            {
                return this.NotFound();
            }
        }
    }
}
