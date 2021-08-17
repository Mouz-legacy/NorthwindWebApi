using System.Collections.Generic;

namespace Northwind.DataAccess.Employees
{
    /// <summary>
    /// Represents a DAO for Northwind employees.
    /// </summary>
#pragma warning disable CA1040
    public interface IEmployeeDataAccessObject
#pragma warning restore CA1040
    {
        /// <summary>
        /// InsertEmployee method.
        /// </summary>
        /// <param name="employee">Employee.</param>
        /// <returns>Employee id.</returns>
        int InsertEmployee(EmployeeTransferObject employee);

        /// <summary>
        /// DeleteEmployee method.
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        bool DeleteEmployee(int employeeId);

        /// <summary>
        /// UpdateEmployee method.
        /// </summary>
        /// <param name="employee">Employee.</param>
        /// <returns>True, if employee was updated.</returns>
        bool UpdateEmployee(EmployeeTransferObject employee);

        /// <summary>
        /// FindEmployee method.
        /// </summary>
        /// <param name="employeeId">Employee id.</param>
        /// <returns>Exists employee.</returns>
        EmployeeTransferObject FindEmployee(int employeeId);

        /// <summary>
        /// SelectEmployees method.
        /// </summary>
        /// <param name="offset">Offset.</param>
        /// <param name="limit">Max limit of employees.</param>
        /// <returns>List of employees.</returns>
        IList<EmployeeTransferObject> SelectEmployees(int offset, int limit);
    }
}
