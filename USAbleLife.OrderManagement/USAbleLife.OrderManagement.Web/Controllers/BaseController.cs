using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using USAbleLife.OrderManagement.Web.Common;

namespace USAbleLife.OrderManagement.Web.Controllers
{
    [NoCache]
    public class BaseController : Controller
    {
        /// <summary>
        /// Called when authorization occurs.
        /// </summary>
        /// <param name="filterContext">Information about the current request and action.</param>
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            System.Diagnostics.Debug.WriteLine($"Hello {filterContext.ActionDescriptor.ActionName}");
            if (filterContext.ActionDescriptor.GetCustomAttributes(false).Any(x => x is IgnoreAttribute))
            {
                base.OnAuthorization(filterContext);
                return;
            }

            if (Session[Constants.SecurityTokenKey] == null)
            {
                filterContext.Result = new RedirectResult("/Login/Index");
            }

            base.OnAuthorization(filterContext);
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            HttpResponseBase response = filterContext.RequestContext.HttpContext.Response;
            response.Write(filterContext.Exception.Message);
            response.ContentType = MediaTypeNames.Text.Plain;
            filterContext.ExceptionHandled = false;
        }

    }
}