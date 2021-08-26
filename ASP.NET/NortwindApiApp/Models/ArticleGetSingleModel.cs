// <copyright file="ArticleGetSingleModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NortwindApiApp.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Northwind.Services.Blogging;
    using Northwind.Services.Employees;

    /// <summary>
    /// ArticleGetSingleModel class.
    /// </summary>
    public class ArticleGetSingleModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArticleGetSingleModel"/> class.
        /// </summary>
        public ArticleGetSingleModel()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArticleGetSingleModel"/> class.
        /// </summary>
        /// <param name="blogArticle">A <see cref="BlogArticle"/>.</param>
        /// <param name="employee">An <see cref="Employee"/>.</param>
        public ArticleGetSingleModel(BlogArticle blogArticle, Employee employee)
        {
            this.Id = blogArticle.BlogArticleId;
            this.Title = blogArticle.Title;
            this.PublicationDate = blogArticle.PublicationDate;
            this.EmployeeId = blogArticle.EmployeeId;
            this.AuthorName = $"{employee.LastName} {employee.FirstName}";
            this.Body = blogArticle.Body;
        }

        /// <summary>
        /// Gets or sets articles id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets articles title.
        /// </summary>
        [StringLength(50)]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets articles publication date.
        /// </summary>
        public DateTime PublicationDate { get; set; }

        /// <summary>
        /// Gets or sets articles employee id.
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// Gets or sets articles author name.
        /// </summary>
        public string AuthorName { get; set; }

        /// <summary>
        /// Gets or sets articles body.
        /// </summary>
        [StringLength(4000)]
        public string Body { get; set; }
    }
}
