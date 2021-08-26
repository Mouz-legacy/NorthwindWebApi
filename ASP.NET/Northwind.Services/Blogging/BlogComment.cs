// <copyright file="BlogComment.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Northwind.Services.Blogging
{
    /// <summary>
    /// BlogComment class.
    /// </summary>
    public class BlogComment
    {
        /// <summary>
        /// Gets or sets blog comment id.
        /// </summary>
        public int BlogCommentId { get; set; }

        /// <summary>
        /// Gets or sets articles id.
        /// </summary>
        public int ArticleId { get; set; }

        /// <summary>
        /// Gets or sets customers id.
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets text.
        /// </summary>
        public string Text { get; set; }
    }
}
