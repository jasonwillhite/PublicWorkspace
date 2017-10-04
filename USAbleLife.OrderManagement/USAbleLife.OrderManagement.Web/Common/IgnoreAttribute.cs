using System;
namespace USAbleLife.OrderManagement.Web.Common
{
    /// <summary>
    /// Used to determine if a Controller Action should require Authenticate prior to executing
    /// </summary>
    /// <seealso cref="System.Attribute" />
    public class IgnoreAttribute : Attribute
    {
    }
}