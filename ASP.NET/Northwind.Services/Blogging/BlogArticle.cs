// <copyright file="BlogArticle.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Northwind.Services.Blogging
{
    using System;

    /// <summary>
    /// BlogArticle entity.
    /// </summary>
    public class BlogArticle
    {
        /// <summary>
        /// Gets or sets id.
        /// </summary>
        public int BlogArticleId { get; set; }

        /// <summary>
        /// Gets or sets title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets body.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Gets or sets publication date.
        /// </summary>
        public DateTime PublicationDate { get; set; }

        /// <summary>
        /// Gets or sets employee id.
        /// </summary>
        public int EmployeeId { get; set; }
    }
}
