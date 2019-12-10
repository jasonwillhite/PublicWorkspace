using System.Collections.Generic;
namespace USAbleLife.OrderManagement.Data.Common
{
    public class SecurityToken
    {
        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<KeyValuePair<string, string>> Claims { get; set; }
    }
}
