using System.Linq;
namespace USAbleLife.OrderManagement.Data.Common
{
    public static class Security
    {
        /// <summary>
        /// Gets the MD5 hash.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        internal static string GetMd5Hash(string input)
        {
            input = "fjbkghfjbnvhgt" + input; // Salt Input
            System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(input);
            bytes = x.ComputeHash(bytes);
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            bytes.ToList().ForEach(b => s.Append(b.ToString("x2").ToLower()));
            string hashvalue = s.ToString();
            return hashvalue;
        }
    }
}
