// <copyright file="DesignTimeBloggingContextFactory.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Northwind.Services.EntityFrameworkCore.Blogging.Context
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// DesignTimeBloggingContextFactory class.
    /// </summary>
    public class DesignTimeBloggingContextFactory : IDesignTimeDbContextFactory<BloggingContext>
    {
        /// <summary>
        /// CreateDbContext method that creates new database context.
        /// </summary>
        /// <param name="args">Command line args.</param>
        /// <returns>New blogging context.</returns>
        public BloggingContext CreateDbContext(string[] args)
        {
            const string connectionStringName = "NORTHWIND_BLOGGING";
            const string connectioStringPrefix = "SQLCONNSTR_";

            var configuration = new ConfigurationBuilder().AddEnvironmentVariables().Build();
            var connectionString = configuration.GetConnectionString(connectionStringName);

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new Exception($"{connectioStringPrefix}{connectionStringName} environment variable is not set.");
            }

            Console.WriteLine($"Using {connectioStringPrefix}{connectionStringName} environment variable as a connection string.");

            var builderOptions = new DbContextOptionsBuilder<BloggingContext>().UseSqlServer(connectionString).Options;
            return new BloggingContext(builderOptions);
        }
    }
}
