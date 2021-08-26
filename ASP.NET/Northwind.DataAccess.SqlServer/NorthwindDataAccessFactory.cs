// <copyright file="NorthwindDataAccessFactory.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Northwind.DataAccess
{
    using Northwind.DataAccess.Employees;
    using Northwind.DataAccess.Products;

    /// <summary>
    /// Represents an abstract factory for creating Northwind DAO.
    /// </summary>
    public abstract class NorthwindDataAccessFactory
    {
        /// <summary>
        /// Gets a DAO for Northwind products.
        /// </summary>
        /// <returns>A <see cref="IProductDataAccessObject"/>.</returns>
        public abstract IProductDataAccessObject GetProductDataAccessObject();

        /// <summary>
        /// Gets a DAO for Northwind product categories.
        /// </summary>
        /// <returns>A <see cref="IProductCategoryDataAccessObject"/>.</returns>
        public abstract IProductCategoryDataAccessObject GetProductCategoryDataAccessObject();

        /// <summary>
        /// Gets a DAO for Northwind employees.
        /// </summary>
        /// <returns>A <see cref="IEmployeeDataAccessObject"/>.</returns>
        public abstract IEmployeeDataAccessObject GetEmployeeDataAccessObject();
    }
}
