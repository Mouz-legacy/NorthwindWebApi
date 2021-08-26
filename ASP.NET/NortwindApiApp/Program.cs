// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

#pragma warning disable CA1052

namespace NortwindApiApp
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;

    /// <summary>
    /// Program class.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main method.
        /// </summary>
        /// <param name="args">Command line args.</param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// CreateHostBuilder method.
        /// </summary>
        /// <param name="args">Command line args.</param>
        /// <returns>new HostBuilder.</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
