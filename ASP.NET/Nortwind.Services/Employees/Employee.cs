// <copyright file="Employee.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Nortwind.Services.Employees
{
    using System;

#pragma warning disable CA1819

    /// <summary>
    /// Represents an employee.
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// Gets or sets employee id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets employee last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets employee first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets employee title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets employee title of countesy.
        /// </summary>
        public string TitleOfCourtesy { get; set; }

        /// <summary>
        /// Gets or sets employees birth date.
        /// </summary>
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// Gets or sets employees hire date.
        /// </summary>
        public DateTime? HireDate { get; set; }

        /// <summary>
        /// Gets or sets employees address.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets employees city.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets employees region.
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// Gets or sets employees postal code.
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        /// Gets or sets employees country.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets employees home phone.
        /// </summary>
        public string HomePhone { get; set; }

        /// <summary>
        /// Gets or sets employees extension.
        /// </summary>
        public string Extension { get; set; }

        /// <summary>
        /// Gets or sets employees photos.
        /// </summary>
        public byte[] Photo { get; set; }

        /// <summary>
        /// Gets or sets employees notes.
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// Gets or sets employees reports.
        /// </summary>
        public int? ReportsTo { get; set; }

        /// <summary>
        /// Gets or sets employees photo path.
        /// </summary>
        public string PhotoPath { get; set; }
    }
}
