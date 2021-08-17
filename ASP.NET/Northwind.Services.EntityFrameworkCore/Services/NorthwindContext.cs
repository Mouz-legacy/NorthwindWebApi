namespace Nortwind.Services.EntityFrameworkCore.Services
{
    using Microsoft.EntityFrameworkCore;
    using Northwind.Services.Products;
    using Nortwind.Services.Products;
    using Nortwind.Services.Employees;

    /// <summary>
    /// Represents a northwind context.
    /// </summary>
    public class NorthwindContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NorthwindContext"/> class.
        /// </summary>
        /// <param name="options">DbContext options.</param>
        public NorthwindContext(DbContextOptions<NorthwindContext> options)
            : base(options)
        {
        }

        public DbSet<ProductCategory> ProductCategories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Employee> Employees { get; set; }
    }
}
