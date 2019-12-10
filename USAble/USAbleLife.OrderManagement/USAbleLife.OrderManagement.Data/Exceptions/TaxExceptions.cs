using System;
namespace USAbleLife.OrderManagement.Data.Exceptions
{
    public class TaxNameException : Exception
    {
        public TaxNameException(string message) : base(message)
        {
        }
    }

    public class TaxPercentageException : Exception
    {
        public TaxPercentageException(string message) : base(message)
        {
        }
    }
}
