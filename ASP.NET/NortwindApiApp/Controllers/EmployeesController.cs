// <copyright file="EmployeesController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NorthwindWebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Northwind.Services.Employees;

    /// <summary>
    /// EmployeesController class.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class EmployeesController : Controller
    {
        private readonly IEmployeeManagementService employeeManagementService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeesController"/> class.
        /// </summary>
        /// <param name="employeeManagementService">Service.</param>
        public EmployeesController(IEmployeeManagementService employeeManagementService)
        {
            this.employeeManagementService = employeeManagementService ?? throw new ArgumentNullException(nameof(employeeManagementService));
        }

        /// <summary>
        /// CreateEmployee method.
        /// </summary>
        /// <param name="employee">Employee.</param>
        /// <returns>Action result.</returns>
        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployeeAsync(Employee employee)
        {
            if (employee is null)
            {
                throw new ArgumentNullException(nameof(employee));
            }

            await this.employeeManagementService.CreateEmployeeAsync(employee).ConfigureAwait(true);
            return this.Ok(employee);
        }

        /// <summary>
        /// GetEmployees method.
        /// </summary>
        /// <param name="offset">Offset.</param>
        /// <param name="limit">Max limit of employees.</param>
        /// <returns>Action result.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployeesAsync(int offset = 0, int limit = 10)
        {
            if (offset >= 0 && limit > 0)
            {
                return this.Ok(await this.employeeManagementService.ShowEmployeesAsync(offset, limit).ConfigureAwait(true));
            }
            else
            {
                return this.BadRequest();
            }
        }

        /// <summary>
        /// GetEmployee method.
        /// </summary>
        /// <param name="employeeId">Employee id.</param>
        /// <returns>Action result.</returns>
        [HttpGet("{employeeId}")]
        public ActionResult<Employee> GetEmployee(int employeeId)
        {
            if (this.employeeManagementService.TryShowEmployee(employeeId, out Employee employee))
            {
                return this.Ok(employee);
            }
            else
            {
                return this.BadRequest();
            }
        }

        /// <summary>
        /// UpdateEmployee method.
        /// </summary>
        /// <param name="employeeId">Employee id.</param>
        /// <param name="employee">Employee.</param>
        /// <returns>Action result.</returns>
        [HttpPut("{employeeId}")]
        public async Task<ActionResult> UpdateEmployeeAsync(int employeeId, Employee employee)
        {
            if (employeeId != employee?.EmployeeId)
            {
                return this.BadRequest();
            }

            await this.employeeManagementService.UpdateEmployeeAsync(employeeId, employee).ConfigureAwait(true);
            return this.NoContent();
        }

        /// <summary>
        /// DeleteEmployee method.
        /// </summary>
        /// <param name="employeeId">Employee id.</param>
        /// <returns>Action result.</returns>
        [HttpDelete("{employeeId}")]
        public async Task<ActionResult<Employee>> DeleteEmployeeAsync(int employeeId)
        {
            if (await this.employeeManagementService.DestroyEmployeeAsync(employeeId).ConfigureAwait(true))
            {
                return this.NoContent();
            }
            else
            {
                return this.NotFound();
            }
        }
    }
}
