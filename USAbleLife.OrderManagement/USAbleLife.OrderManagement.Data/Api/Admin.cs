using USAbleLife.OrderManagement.Data.DataAccess;
namespace USAbleLife.OrderManagement.Data.Api
{
    public static class Admin
    {
        /// <summary>
        /// Logins the specified username.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public static Employee Login(string username, string password)
        {
            return Employee.Login(username, password);
        }

        /// <summary>
        /// Gets the employee.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public static Employee GetEmployee(long id)
        {
            return Employee.GetEmployee(id);
        }
    }
}
