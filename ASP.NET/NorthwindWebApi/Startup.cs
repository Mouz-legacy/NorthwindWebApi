// <copyright file="Startup.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Northwind.DataAccess.SqlServer.Employees;
using Nortwind.Services.Employees;

namespace NorthwindWebApi
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.OpenApi.Models;
    using Northwind.DataAccess;
    using Northwind.Services.DataAccess;
    using Northwind.Services.Products;
    using Nortwind.Services.EntityFrameworkCore.Services;
    using Nortwind.Services.Products;

    /// <summary>
    /// Startup class.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">config.</param>
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        /// <summary>
        /// Gets config.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// ConfigureServices method.
        /// </summary>
        /// <param name="services">Services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped((service) =>
            {
                var sqlConnection = new System.Data.SqlClient.SqlConnection(this.Configuration.GetConnectionString("NorthwindConnection"));
                sqlConnection.Open();
                return sqlConnection;
            });

            services.AddTransient<NorthwindDataAccessFactory, SqlServerDataAccessFactory>();
            services.AddTransient<IProductManagementService, ProductManagementDataAccessService>();
            services.AddTransient<IProductCategoryManagementService, ProductCategoriesManagementDataAccessService>();
            services.AddTransient<IProductCategoryPicturesService, ProductCategoryPicturesManagementDataAccessService>();
            services.AddTransient<IEmployeeManagementService, EmployeesManagementDataAccessService>();
            services.AddControllers();
            services.AddDbContext<NorthwindContext>(opt => opt.UseInMemoryDatabase("Northwind"));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "NorthwindWebApi", Version = "v1" });
            });
        }

        /// <summary>
        /// Configure method.
        /// </summary>
        /// <param name="app">App.</param>
        /// <param name="env">Environment.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "NorthwindWebApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
