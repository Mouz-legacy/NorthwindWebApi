// <copyright file="Category.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

#pragma warning disable SA1600
#pragma warning disable CA1819

namespace Northwind.Services.Products
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Category class.
    /// </summary>
    public partial class Category
    {
        [Key]
        [Column("CategoryID")]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(15)]
        public string CategoryName { get; set; }

        [Column(TypeName = "ntext")]
        public string Description { get; set; }

        [Column(TypeName = "image")]
        public byte[] Picture { get; set; }
    }
}