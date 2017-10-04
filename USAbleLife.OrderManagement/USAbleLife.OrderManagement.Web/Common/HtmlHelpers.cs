using System;

namespace USAbleLife.OrderManagement.Web.Common
{
    public static class HtmlHelpers
    {
        public static string PaddedString(string left, string right, char padding, int length)
        {
            return $"{left.PadRight(Math.Max(0, length - (left.Length + right.Length)), padding)}{right}";
        }
    }
}