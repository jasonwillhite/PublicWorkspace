using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using USAbleLife.OrderManagement.Data.DataAccess;
using USAbleLife.OrderManagement.Web.Common;
using USAbleLife.OrderManagement.Web.Models;

namespace USAbleLife.OrderManagement.Web.Controllers
{
    public class OrderController : BaseController
    {
        public ActionResult Index()
        {
            OrderCart orderCart;
            Session[Constants.OrderCartKey] = orderCart = new OrderCart();
            return View(orderCart);
        }

        /// <summary>
        /// Gets the Order Summary
        /// </summary>
        /// <returns></returns>
        public PartialViewResult OrderSummary()
        {
            OrderCart orderCart = (OrderCart)Session[Constants.OrderCartKey] ?? new OrderCart();
            return PartialView("OrderSummary", orderCart);
        }

        /// <summary>
        /// Adds the item to order.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public PartialViewResult AddItemToOrder(int id)
        {
            return AlterOrder(x => x.SelectedMenuItems.Add(x.AvailableMenuItems.FirstOrDefault(y => y.Id == id)));
        }

        /// <summary>
        /// Removes the item from order.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public PartialViewResult RemoveItemFromOrder(int id)
        {
            return AlterOrder(x => x.SelectedMenuItems.Remove(x.AvailableMenuItems.FirstOrDefault(y => y.Id == id)));
        }

        /// <summary>
        /// Adds the discount to order.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public PartialViewResult AddDiscountToOrder(int id)
        {
            return AlterOrder(x => x.SelectedDiscount = x.AvailableDiscounts.FirstOrDefault(y => y.Id == id));
        }

        /// <summary>
        /// Adds the tax to order.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public PartialViewResult AddTaxToOrder(int id)
        {
            return AlterOrder(x => x.SelectedTaxes.Add(x.AvailableTaxes.FirstOrDefault(y => y.Id == id)));
        }

        /// <summary>
        /// Removes the tax from order.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public PartialViewResult RemoveTaxFromOrder(int id)
        {
            return AlterOrder(x => x.SelectedTaxes.Remove(x.AvailableTaxes.FirstOrDefault(y => y.Id == id)));
        }

        /// <summary>
        /// Alters the order.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        internal PartialViewResult AlterOrder(Action<OrderCart> action)
        {
            OrderCart orderCart = (OrderCart)Session[Constants.OrderCartKey] ?? new OrderCart();
            if (orderCart == null)
            {
                return PartialView("OrderSummary", new OrderCart());
            }

            action(orderCart);
            Session[Constants.OrderCartKey] = orderCart;
            return PartialView("OrderSummary", orderCart);
        }

        /// <summary>
        /// Clears the order.
        /// </summary>
        /// <returns></returns>
        public PartialViewResult ClearOrder()
        {
            OrderCart orderCart;
            Session[Constants.OrderCartKey] = orderCart = new OrderCart();
            return PartialView("OrderSummary", orderCart);
        }

        /// <summary>
        /// Submits the order.
        /// </summary>
        /// <returns></returns>
        public ActionResult SubmitOrder()
        {
            Employee employee = Session[Constants.SecurityTokenKey] as Employee;
            OrderCart orderCart = Session[Constants.OrderCartKey] as OrderCart;
            if (orderCart == null || !orderCart.SelectedMenuItems.Any())
            {
                return Json(new AjaxResult { Error = "Order is empty", Success = false, Value = "Order is empty" });
            }

            MealOrder mealOrder = new MealOrder { Created = DateTime.Now, EmployeeId = (employee ?? new Employee()).Id, DiscountId = orderCart.SelectedDiscount.Id };
            List<MealOrderItem> mealOrderItems = orderCart.SelectedMenuItems.GroupBy(x => x.Id).Select(x => new MealOrderItem { MenuItemId = x.Key, Quantity = x.ToList().Count }).ToList();
            List<MealOrderTax> mealOrderTaxes = orderCart.SelectedTaxes.Select(x => new MealOrderTax { TaxId = x.Id }).ToList();

            long mealOrderid = Data.Api.Order.SubmitOrder(mealOrder, mealOrderItems, mealOrderTaxes);
            return Json(new AjaxResult { Error = string.Empty, Success = true, Value = $"/Home/Index/{mealOrderid}" });
        }
    }
}