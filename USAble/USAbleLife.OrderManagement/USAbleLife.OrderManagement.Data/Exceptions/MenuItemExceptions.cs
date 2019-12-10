using System;
namespace USAbleLife.OrderManagement.Data.Exceptions
{
    public class MenuItemNameException : Exception
    {
        public MenuItemNameException(string message) : base(message) { }
    }

    public class MenuItemPriceException : Exception
    {
        public MenuItemPriceException(string message) : base(message) { }
    }
}
