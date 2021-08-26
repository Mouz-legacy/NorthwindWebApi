// <copyright file="ArticleUpdateModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NortwindApiApp.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Northwind.Services.Blogging;

    /// <summary>
    /// ArticleUpdateModel class.
    /// </summary>
    public class ArticleUpdateModel
    {
        /// <summary>
        /// Gets or sets articles title.
        /// </summary>
        [StringLength(50)]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets articles body.
        /// </summary>
        [StringLength(4000)]
        public string Body { get; set; }

        /// <summary>
        /// Implicit operator to convert to blog article.
        /// </summary>
        /// <param name="blogArticleUpdateQuery">Current instance.</param>
        public static implicit operator BlogArticle(ArticleUpdateModel blogArticleUpdateQuery)
        {
            return new BlogArticle
            {
                Title = blogArticleUpdateQuery.Title,
                Body = blogArticleUpdateQuery.Body,
                PublicationDate = DateTime.Now,
            };
        }
    }
}
