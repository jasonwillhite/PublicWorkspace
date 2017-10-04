using System;
using System.Web.Mvc;
using USAbleLife.OrderManagement.Data.Api;
using USAbleLife.OrderManagement.Data.DataAccess;
using USAbleLife.OrderManagement.Web.Models;

namespace USAbleLife.OrderManagement.Web.Controllers
{
    public class AdminController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Gets all discounts
        /// </summary>
        /// <returns></returns>
        public PartialViewResult Discounts()
        {
            return PartialView("AdminDiscounts", Order.GetAllDiscounts(autoLoadNoneDiscount: false));
        }

        /// <summary>
        /// Saves the discount.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="amount">The amount.</param>
        /// <param name="type">The type.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ActionResult SaveDiscount(string name, string amount, string type, string id)
        {
            long discountId;
            long.TryParse(id, out discountId);
            try
            {
                Order.SaveDiscount(new Discount { Amount = decimal.Parse(amount), IsDeleted = false, Id = discountId, Name = name, Type = int.Parse(type) });
                return Json(new AjaxResult { Error = string.Empty, Success = true});
            }
            catch (Exception ex)
            {
                return Json(new AjaxResult { Error = ex.Message, Success = false, Value = ex.Message });
            }
        }

        /// <summary>
        /// Deletes the discount.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public PartialViewResult DeleteDiscount(string id)
        {
            Order.DeleteDiscount(long.Parse(id));
            return PartialView("AdminDiscounts", Order.GetAllDiscounts(autoLoadNoneDiscount: false));
        }




        /// <summary>
        /// Gets all the taxes
        /// </summary>
        /// <returns></returns>
        public PartialViewResult Taxes()
        {
            return PartialView("AdminTaxes", Order.GetAllTaxes());
        }

        /// <summary>
        /// Saves the tax.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="amount">The amount.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ActionResult SaveTax(string name, string amount, string id)
        {
            try
            {
                long taxId;
                long.TryParse(id, out taxId);
                Tax tax = new Tax { Id = taxId, IsDeleted = false, Name = name, Percentage = int.Parse(amount)};
                Order.SaveTax(tax);
                return Json(new AjaxResult { Error = string.Empty, Success = true });

            }
            catch (Exception ex)
            {
                return Json(new AjaxResult { Error = ex.Message, Success = false, Value = ex.Message});
            }
        }

        /// <summary>
        /// Deletes the tax.
        /// </summary>
        /// <returns></returns>
        public PartialViewResult DeleteTax(string id)
        {
            Order.DeleteTax(long.Parse(id));
            return PartialView("AdminTaxes", Order.GetAllTaxes());
        }




        /// <summary>
        /// Gets all Employees
        /// </summary>
        /// <returns></returns>
        public PartialViewResult Employees()
        {
            return PartialView("AdminEmployees", Order.GetAllEmployees());
        }

        /// <summary>
        /// Saves the employee.
        /// </summary>
        /// <returns></returns>
        public ActionResult SaveEmployee(string username, string firstname, string lastname, string changePassword, string password, string id)
        {
            try
            {
                long employeeId;
                long.TryParse(id, out employeeId);
                Employee employee = new Employee {FirstName = firstname, LastName = lastname, IsDeleted = false, Id = employeeId, Username = username, Password = password};
                Order.SaveEmployee(employee, bool.Parse(changePassword));
                return Json(new AjaxResult { Error = string.Empty, Success = true });
            }
            catch (Exception ex)
            {
                return Json(new AjaxResult { Error = ex.Message, Success = false, Value = ex.Message});
            }
        }

        /// <summary>
        /// Deletes the employee.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public PartialViewResult DeleteEmployee(string id)
        {
            Order.DeleteEmployee(long.Parse(id));
            return PartialView("AdminEmployees", Order.GetAllEmployees());
        }




        /// <summary>
        /// Gets the menu items
        /// </summary>
        /// <returns></returns>
        public PartialViewResult MenuItems()
        {
            return PartialView("AdminMenuItems", Order.GetAllMenuItems());
        }

        /// <summary>
        /// Saves the menu item.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="price">The price.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ActionResult SaveMenuItem(string name, string price, string id)
        {
            try
            {
                long menuItemId;
                long.TryParse(id, out menuItemId);
                MenuItem menuItem = new MenuItem { Id = menuItemId, IsDeleted = false, Name = name, Price = decimal.Parse(price)};
                Order.SaveMenuItem(menuItem);
                return Json(new AjaxResult { Error = string.Empty, Success = true });
            }
            catch (Exception ex)
            {
                return Json(new AjaxResult { Error = ex.Message, Success = false, Value = ex.Message});
            }
        }

        /// <summary>
        /// Deletes the menu item.
        /// </summary>
        /// <returns></returns>
        public PartialViewResult DeleteMenuItem(string id)
        {
            Order.DeleteMenuItem(long.Parse(id));
            return PartialView("AdminMenuItems", Order.GetAllMenuItems());
        }
    }
}