// <copyright file="ArticleCreateModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NortwindApiApp.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Northwind.Services.Blogging;

    /// <summary>
    /// BlogArticleProvider to provide operation with db.
    /// </summary>
    public class ArticleCreateModel
    {
        /// <summary>
        /// Gets or sets title property.
        /// </summary>
        [StringLength(50)]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets body property.
        /// </summary>
        [StringLength(4000)]
        public string Body { get; set; }

        /// <summary>
        /// Gets or sets employee id.
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// Public implicit operator to Blog article.
        /// </summary>
        /// <param name="blogArticle">Current instance.</param>
        public static implicit operator BlogArticle(ArticleCreateModel blogArticle)
        {
            return new BlogArticle
            {
                BlogArticleId = 0,
                Title = blogArticle?.Title,
                Body = blogArticle?.Body,
                PublicationDate = DateTime.Now,
                EmployeeId = blogArticle.EmployeeId,
            };
        }
    }
}
