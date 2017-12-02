using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using USAbleLife.OrderManagement.Data.Common;
using USAbleLife.OrderManagement.Data.Exceptions;
namespace USAbleLife.OrderManagement.Data.DataAccess
{
    public partial class Employee
    {
        /// <summary>
        /// Logs in the specified employee.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        internal static Employee Login(string username, string password)
        {
            OrderManagementDataContext context = new OrderManagementDataContext();
            string hashPassword = Security.GetMd5Hash(password);
            Employee employee = context.Employees.FirstOrDefault(x => x.Username == username && x.Password == hashPassword && x.IsDeleted == false);
            return employee == null ? null : new Employee { FirstName = employee.FirstName, Username = employee.Username, LastName = employee.LastName, Id = employee.Id };
        }

        /// <summary>
        /// Gets the employee.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        internal static Employee GetEmployee(long id)
        {
            Employee employee = new OrderManagementDataContext().Employees.FirstOrDefault(x => x.Id == id);
            if (employee != null)
            {
                employee.Password = string.Empty;
            }
            return employee;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }

        /// <summary>
        /// Gets all employees.
        /// </summary>
        /// <returns></returns>
        internal static List<Employee> GetAllEmployees()
        {
            OrderManagementDataContext context = new OrderManagementDataContext();
            return context.Employees.Where(x => x.IsDeleted == false).ToList();
        }

        /// <summary>
        /// Determines if the Employee's username is unique
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// 'True' if it is unique, else 'False'
        /// </returns>
        internal static bool UsernameIsUnique(string username, long id)
        {
            OrderManagementDataContext context = new OrderManagementDataContext();
            return !context.Employees.Where(x => x.Id != id).Where(x => x.IsDeleted == false).Any(x => x.Username == username);
        }

        /// <summary>
        /// Determines if the username is valid
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns></returns>
        internal static bool EmployeeStringIsValid(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return false;
            }

            Regex regex = new Regex("^[a-zA-Z0-9]{1,25}$");
            return regex.Match(username).Success;
        }

        /// <summary>
        /// Employees the username is valid.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns></returns>
        internal static bool EmployeeUsernameIsValid(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return false;
            }

            Regex regex = new Regex("^[0-9]{4}$");
            return regex.Match(username).Success;
        }

        /// <summary>
        /// Determines if the password is valid
        /// </summary>
        /// <param name="password">The password.</param>
        /// <returns>'True' if the password is valid, else 'False'</returns>
        internal static bool PasswordIsValid(string password) => !string.IsNullOrEmpty(password) && password.Length >= 4 && password.Length <= 16;

        /// <summary>
        /// Saves the employee.
        /// </summary>
        /// <param name="employee">The employee.</param>
        /// <param name="updatePassword">if set to <c>true</c> [update password].</param>
        internal static void SaveEmployee(Employee employee, bool updatePassword = false)
        {
            // Only validate password on Employee creation or when the user explicitly updates it
            employee.ValidateEmployee(updatePassword || employee.Id <= 0);
            OrderManagementDataContext context = new OrderManagementDataContext();
            if (employee.Id <= 0)
            {
                employee.Password = Security.GetMd5Hash(employee.Password);
                context.Employees.InsertOnSubmit(employee);
            }
            else
            {
                Employee existingEmployee = context.Employees.FirstOrDefault(x => x.Id == employee.Id);
                if (existingEmployee != null)
                {
                    if (updatePassword)
                    {
                        existingEmployee.Password = Security.GetMd5Hash(employee.Password);
                    }
                    existingEmployee.Username = employee.Username;
                    existingEmployee.FirstName = employee.FirstName;
                    existingEmployee.LastName = employee.LastName;
                }
            }
            context.SubmitChanges();
        }

        /// <summary>
        /// Deletes the employee.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <exception cref="System.NotSupportedException">There needs be at least one employee in the system</exception>
        internal static void DeleteEmployee(long id)
        {
            OrderManagementDataContext context = new OrderManagementDataContext();
            if (context.Employees.Count() == 1)
            {
                throw new NotSupportedException("There needs be at least one employee in the system");
            }
            Employee employee = context.Employees.FirstOrDefault(x => x.Id == id);
            if (employee != null)
            {
                employee.IsDeleted = true;
                context.SubmitChanges();
            }
        }
    }

    internal static class EmployeeExtensions
    {
        /// <summary>
        /// Validates the employee.
        /// </summary>
        /// <param name="employee">The employee.</param>
        /// <param name="validatePassword">if set to <c>true</c> [validate password].</param>
        /// <exception cref="System.NullReferenceException"></exception>
        /// <exception cref="EmployeeNameException">
        /// Employee name must be unique
        /// or
        /// Employee name must be a valid 4 digit number
        /// </exception>
        /// <exception cref="EmployeePasswordException">Password must be a 4 to 16 characters long</exception>
        internal static void ValidateEmployee(this Employee employee, bool validatePassword = true)
        {
            if (employee == null)
            {
                throw new NullReferenceException(nameof(employee));
            }

            if (!Employee.UsernameIsUnique(employee.Username, employee.Id))
            {
                throw new EmployeeNameException("Employee name must be unique");
            }

            if (!Employee.EmployeeUsernameIsValid(employee.Username))
            {
                throw new EmployeeNameException("Employee name must be a valid 4 digit number");
            }

            if (validatePassword && !Employee.PasswordIsValid(employee.Password))
            {
                throw new EmployeePasswordException("Password must be a 4 to 16 characters long");
            }

            if (string.IsNullOrEmpty(employee.FirstName) || string.IsNullOrEmpty(employee.LastName) || !Employee.EmployeeStringIsValid(employee.FirstName) || !Employee.EmployeeStringIsValid(employee.LastName))
            {
                throw new EmployeeNameException("Employee first name and last name must not be empty and must be 1 to 25 characters long");
            }
        }
    }
}
