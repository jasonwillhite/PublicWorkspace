using System;
namespace USAbleLife.OrderManagement.Data.Exceptions
{
    public class DiscountNameException : Exception
    {
        public DiscountNameException(string message) : base(message) { }
    }

    public class DiscountAmountException : Exception
    {
        public DiscountAmountException(string message) : base(message) { }
    }
}
