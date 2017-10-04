using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using USAbleLife.OrderManagement.Data.Api;
using USAbleLife.OrderManagement.Data.DataAccess;
using USAbleLife.OrderManagement.Web.Common;
using USAbleLife.OrderManagement.Web.Models;

namespace USAbleLife.OrderManagement.Web.Controllers
{
    public class HomeController : BaseController
    {
        /// <summary>
        /// Returns the Order Meal dashboard
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="id">The identifier of the newly created Meal Order. If none was created, then -1</param>
        /// <returns></returns>
        public ActionResult Index(int page = 1, long id = -1)
        {
            List<MealOrder> orders = Order.GetMealOrders().OrderByDescending(x => x.Created).ToList();
            MealOrderPagingResult result = new MealOrderPagingResult
            {
                CurrentPage = page,
                PageSize = Constants.OrderMealDashboardPageSize,
                TotalNumberOfMealOrders = orders.Count,
                MealOrders = orders.Skip((page - 1) * Constants.OrderMealDashboardPageSize).Take(Constants.OrderMealDashboardPageSize).ToList()
            };

            ViewBag.NewMealOrderId = id;
            return View(result);
        }

        /// <summary>
        /// Gets the order summary.
        /// </summary>
        /// <param name="mealOrderId">The meal order identifier.</param>
        /// <returns></returns>
        public ActionResult GetOrderSummary(long mealOrderId = -1)
        {
            MealOrder mealOrder = Order.GetMealOrder(mealOrderId);
            if (mealOrder == null)
            {
                return PartialView("OrderSummary", new OrderCart());
            }

            OrderCart orderCart = new OrderCart { CanSumbit = false };
            Parallel.Invoke
            (
               () => { orderCart.SelectedDiscount = mealOrder.DiscountId.HasValue ? Order.GetDiscount(mealOrder.DiscountId.Value) : null; },
               () => { orderCart.SelectedMenuItems = Order.GetMenuItems(mealOrder.Id); },
               () => { orderCart.SelectedTaxes = new HashSet<Tax>(Order.GetMealOrderTaxes(mealOrder.Id)); }
            );

            return PartialView("OrderSummary", orderCart);
        }
    }
}