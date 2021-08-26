// <copyright file="EmployeesManagementDataAccessService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Northwind.DataAccess.SqlServer.Employees
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Threading.Tasks;
    using Northwind.DataAccess.Employees;
    using Northwind.Services.Employees;

    /// <summary>
    /// EmployeesManagementDataAccessService class.
    /// </summary>
    public class EmployeesManagementDataAccessService : IEmployeeManagementService
    {
        private readonly NorthwindDataAccessFactory northwindDataAccessFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeesManagementDataAccessService"/> class.
        /// </summary>
        /// <param name="sqlConnection">Sql connection.</param>
        public EmployeesManagementDataAccessService(SqlConnection sqlConnection)
        {
            this.northwindDataAccessFactory = new SqlServerDataAccessFactory(sqlConnection) ?? throw new ArgumentNullException(nameof(sqlConnection));
        }

        /// <inheritdoc/>
        public async Task<int> CreateEmployeeAsync(Employee employee)
        {
            if (employee is null)
            {
                throw new ArgumentNullException(nameof(employee));
            }

            return await this.northwindDataAccessFactory.GetEmployeeDataAccessObject().InsertEmployeeAsync((EmployeeTransferObject)employee).ConfigureAwait(true);
        }

        /// <inheritdoc/>
        public async Task<bool> DestroyEmployeeAsync(int employeeId)
        {
            if (employeeId < 1)
            {
                throw new ArgumentException("EmployeeId can't be less than one.", nameof(employeeId));
            }

            return await this.northwindDataAccessFactory.GetEmployeeDataAccessObject().DeleteEmployeeAsync(employeeId).ConfigureAwait(true);
        }

        /// <inheritdoc/>
        public async Task<IList<Employee>> ShowEmployeesAsync(int offset, int limit)
        {
            var employees = new List<Employee>();
            foreach (var employee in await this.northwindDataAccessFactory.GetEmployeeDataAccessObject().SelectEmployeesAsync(offset, limit).ConfigureAwait(true))
            {
                employees.Add((Employee)employee);
            }

            return employees;
        }

        /// <inheritdoc/>
        public bool TryShowEmployee(int employeeId, out Employee employee)
        {
            if (employeeId < 1)
            {
                throw new ArgumentException("EmployeeId can't be less than one.", nameof(employeeId));
            }

            try
            {
                employee = (Employee)this.northwindDataAccessFactory.GetEmployeeDataAccessObject().FindEmployee(employeeId);
            }
            catch
            {
                employee = null;
                return false;
            }

            return true;
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateEmployeeAsync(int employeeId, Employee employee)
        {
            if (employee is null)
            {
                throw new ArgumentNullException(nameof(employee));
            }

            if (employeeId != employee.EmployeeId)
            {
                return false;
            }

            if (await this.northwindDataAccessFactory.GetEmployeeDataAccessObject().UpdateEmployeeAsync((EmployeeTransferObject)employee).ConfigureAwait(true))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
