// <copyright file="BloggingContext.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Northwind.Services.EntityFrameworkCore.Blogging.Context
{
    using Microsoft.EntityFrameworkCore;
    using Northwind.Services.Blogging;

    /// <summary>
    /// BloggingContext class.
    /// </summary>
    public class BloggingContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BloggingContext"/> class.
        /// </summary>
        /// <param name="options">Db options.</param>
        public BloggingContext(DbContextOptions<BloggingContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets BlogArticles property.
        /// </summary>
        public DbSet<BlogArticle> Articles { get; set; }

        /// <summary>
        /// Gets or sets Articles products.
        /// </summary>
        public DbSet<BlogArticleProduct> ArticleProducts { get; set; }

        /// <summary>
        /// Gets or sets Blog comments.
        /// </summary>
        public DbSet<BlogComment> Comments { get; set; }

        /// <summary>
        /// OnModelCreating method that create a new model.
        /// </summary>
        /// <param name="modelBuilder">Model builder instance.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlogArticle>().HasKey(a => a.BlogArticleId);
            modelBuilder.Entity<BlogArticleProduct>().HasKey(a => a.BlogArticleProductId);
            modelBuilder.Entity<BlogComment>().HasKey(a => a.BlogCommentId);

            modelBuilder.Entity<BlogArticle>(
                en =>
                {
                    en.Property(p => p.BlogArticleId)
                        .HasColumnType("int")
                        .HasColumnName("blog_article_id");
                    en.Property(p => p.Title)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("title");
                    en.Property(p => p.Body)
                        .HasColumnType("nvarchar(4000)")
                        .HasColumnName("body");
                    en.Property(p => p.PublicationDate)
                        .HasColumnType("date")
                        .HasColumnName("publication_date");
                    en.Property(p => p.EmployeeId)
                        .HasColumnType("int")
                        .HasColumnName("employee_id");
                });

            modelBuilder.Entity<BlogArticleProduct>(
                en =>
                {
                    en.Property(p => p.BlogArticleProductId)
                        .HasColumnType("int")
                        .HasColumnName("blog_article_product_id");
                    en.Property(p => p.ArticleId)
                        .HasColumnType("int")
                        .HasColumnName("article_id");
                    en.Property(p => p.ProductId)
                        .HasColumnType("int")
                        .HasColumnName("product_id");
                });

            modelBuilder.Entity<BlogComment>(
                en =>
                {
                    en.Property(p => p.BlogCommentId)
                        .HasColumnType("int")
                        .HasColumnName("blog_comment_id");
                    en.Property(p => p.ArticleId)
                        .HasColumnType("int")
                        .HasColumnName("article_id");
                    en.Property(p => p.Text)
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("text");
                    en.Property(p => p.CustomerId)
                        .HasColumnType("int")
                        .HasColumnName("customer_id");
                });
        }
    }
}
