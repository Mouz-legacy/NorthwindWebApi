// <copyright file="NorthwindContext.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

#nullable disable
#pragma warning disable SA1600

namespace Northwind.Services.EntityFrameworkCore.Context
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Northwind.Services.Employees;
    using Northwind.Services.Products;

    /// <summary>
    /// NorthwindContext class.
    /// </summary>
    public partial class NorthwindContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NorthwindContext"/> class.
        /// </summary>
        public NorthwindContext()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NorthwindContext"/> class.
        /// </summary>
        /// <param name="options">Options.</param>
        public NorthwindContext(DbContextOptions<NorthwindContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<Employee> Employees { get; set; }

        public virtual DbSet<Product> Products { get; set; }

        /// <summary>
        /// OnModelCreating method.
        /// </summary>
        /// <param name="modelBuilder">Model builder.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder is null)
            {
                throw new ArgumentNullException(nameof(modelBuilder));
            }

            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            this.OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
