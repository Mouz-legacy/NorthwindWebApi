// <copyright file="IEmployeeManagementService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Northwind.Services.Employees
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// IEmployeeManagementService interface.
    /// </summary>
    public interface IEmployeeManagementService
    {
        /// <summary>
        /// ShowEmployees method.
        /// </summary>
        /// <param name="offset">Offset employees.</param>
        /// <param name="limit">Max limit.</param>
        /// <returns>List of employees.</returns>
        Task<IList<Employee>> ShowEmployeesAsync(int offset, int limit);

        /// <summary>
        /// TryShowEmployee method.
        /// </summary>
        /// <param name="employeeId">Employee id.</param>
        /// <param name="employee">Employee.</param>
        /// <returns>True, if exist.</returns>
        bool TryShowEmployee(int employeeId, out Employee employee);

        /// <summary>
        /// CreateEmployee method.
        /// </summary>
        /// <param name="employee">Employee.</param>
        /// <returns>Number of employee.</returns>
        Task<int> CreateEmployeeAsync(Employee employee);

        /// <summary>
        /// DestroyEmployee method.
        /// </summary>
        /// <param name="employeeId">Employee id.</param>
        /// <returns>True, if employee were destroyed.</returns>
        Task<bool> DestroyEmployeeAsync(int employeeId);

        /// <summary>
        /// UpdateEmployee method.
        /// </summary>
        /// <param name="employeeId">Employee id.</param>
        /// <param name="employee">Employee.</param>
        /// <returns>True, if employee was updated.</returns>
        Task<bool> UpdateEmployeeAsync(int employeeId, Employee employee);
    }
}
