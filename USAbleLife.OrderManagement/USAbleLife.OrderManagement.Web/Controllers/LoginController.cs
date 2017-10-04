using System.Web.Mvc;
using USAbleLife.OrderManagement.Data.Api;
using USAbleLife.OrderManagement.Data.DataAccess;
using USAbleLife.OrderManagement.Web.Common;
using USAbleLife.OrderManagement.Web.Models;

namespace USAbleLife.OrderManagement.Web.Controllers
{
    public class LoginController : BaseController
    {
        [Ignore]
        public ActionResult Index()
        {
            if (Session[Constants.SecurityTokenKey] != null)
            {
                return Redirect("/Home/Index");
            }
            return View();
        }

        /// <summary>
        /// Logs in the user
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        [Ignore]
        public ActionResult UserLogin(string username, string password)
        {
            Employee securityToken = Admin.Login(username, password);
            if (securityToken != null)
            {
                Session[Constants.SecurityTokenKey] = securityToken;
                return Json(new AjaxResult { Success = true, Value = "Logged in" });
            }

            return Json(new AjaxResult { Success = false, Value = "Username or password was incorrect" });
        }

        public ActionResult Logout()
        {
            Session[Constants.SecurityTokenKey] = null;
            return new RedirectResult("/Login");
        }
    }
}