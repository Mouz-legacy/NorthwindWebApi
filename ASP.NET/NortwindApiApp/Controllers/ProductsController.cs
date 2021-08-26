// <copyright file="ProductsController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NorthwindWebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
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
        public async Task<ActionResult<Product>> CreateProductAsync(Product product)
        {
            if (product is null)
            {
                return this.BadRequest();
            }

            await this.productManagementService.CreateProductAsync(product).ConfigureAwait(true);
            return this.Ok(product);
        }

        /// <summary>
        /// GetProducts method.
        /// </summary>
        /// <param name="offset">Offset.</param>
        /// <param name="limit">Limit data.</param>
        /// <returns>Action result.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsAsync(int offset = 0, int limit = 10)
        {
            if (offset >= 0 && limit > 0)
            {
                return this.Ok(await this.productManagementService.ShowProductsAsync(offset, limit).ConfigureAwait(true));
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
        public async Task<ActionResult<IEnumerable<Product>>> GetProductAsync(int productId)
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
        public async Task<ActionResult> UpdateProductAsync(int productId, Product product)
        {
            if (productId != product?.ProductId)
            {
                return this.BadRequest();
            }

            await this.productManagementService.UpdateProductAsync(productId, product).ConfigureAwait(true);
            return this.NoContent();
        }

        /// <summary>
        /// DeleteProduct method.
        /// </summary>
        /// <param name="productId">Product id.</param>
        /// <returns>Action result.</returns>
        [HttpDelete("{productId}")]
        public async Task<ActionResult<Product>> DeleteProductAsync(int productId)
        {
            if (await this.productManagementService.DestroyProductAsync(productId).ConfigureAwait(true))
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
