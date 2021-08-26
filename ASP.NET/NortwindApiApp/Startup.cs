// <copyright file="Startup.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NortwindApiApp
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.OpenApi.Models;
    using Northwind.Services.Blogging;
    using Northwind.Services.Employees;
    using Northwind.Services.EntityFrameworkCore.Blogging;
    using Northwind.Services.EntityFrameworkCore.Blogging.Context;
    using Northwind.Services.EntityFrameworkCore.Context;
    using Northwind.Services.EntityFrameworkCore.Services;
    using Northwind.Services.Products;
    using Nortwind.Services.EntityFrameworkCore.Services;

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
            var northwindConnectionString = this.Configuration.GetConnectionString("NorthwindConnection");
            services.AddDbContext<NorthwindContext>(o => o.UseSqlServer(northwindConnectionString));
            var bloggingConnectionString = this.Configuration.GetConnectionString("BloggingConnection");
            services.AddDbContext<BloggingContext>(o => o.UseSqlServer(bloggingConnectionString));

            services.AddTransient<IProductManagementService, ProductManagementService>();
            services.AddTransient<IProductCategoryManagementService, ProductCategoryManagementService>();
            services.AddTransient<IProductCategoryPicturesService, ProductCategoryPicturesService>();
            services.AddTransient<IEmployeeManagementService, EmployeeManagementService>();
            services.AddTransient<IBloggingService, BloggingService>();

            services.AddControllers();
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
