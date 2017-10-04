using System;
namespace USAbleLife.OrderManagement.Data.Exceptions
{
    public class EmployeeNameException : Exception
    {
        public EmployeeNameException(string message) : base(message) { }
    }

    public class EmployeePasswordException : Exception
    {
        public EmployeePasswordException(string message) : base(message) { }
    }
}
