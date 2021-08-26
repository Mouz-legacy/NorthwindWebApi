// <copyright file="Product.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Northwind.Services.Products
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Represents a product.
    /// </summary>
    public partial class Product
    {
        /// <summary>
        /// Gets or sets a product identifier.
        /// </summary>
        [Key]
        [Column("ProductID")]
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets a product name.
        /// </summary>
        [StringLength(50)]
        public string ProductName { get; set; }

        /// <summary>
        /// Gets or sets a supplier identifier.
        /// </summary>
        [Column("SupplierID")]
        public int? SupplierId { get; set; }

        /// <summary>
        /// Gets or sets a category identifier.
        /// </summary>
        [Column("CategoryID")]
        public int? CategoryId { get; set; }

        /// <summary>
        /// Gets or sets a quantity per unit.
        /// </summary>
        [StringLength(50)]
        public string QuantityPerUnit { get; set; }

        /// <summary>
        /// Gets or sets a unit price.
        /// </summary>
        [Column(TypeName = "money")]
        public decimal? UnitPrice { get; set; }

        /// <summary>
        /// Gets or sets an amount of units in stock.
        /// </summary>
        public short? UnitsInStock { get; set; }

        /// <summary>
        /// Gets or sets an amount of units on order.
        /// </summary>
        public short? UnitsOnOrder { get; set; }

        /// <summary>
        /// Gets or sets a reorder level.
        /// </summary>
        public short? ReorderLevel { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a product is discontinued.
        /// </summary>
        public bool Discontinued { get; set; }
    }
}