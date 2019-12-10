using System.Collections.Generic;
using USAbleLife.OrderManagement.Data.DataAccess;
namespace USAbleLife.OrderManagement.Data.Api
{
    public static class Order
    {
        /// <summary>
        /// Gets all discounts.
        /// </summary>
        /// <param name="autoLoadNoneDiscount">if set to <c>true</c> [automatic load none discount].</param>
        /// <returns></returns>
        public static List<Discount> GetAllDiscounts(bool autoLoadNoneDiscount = true)
        {
            return Discount.GetAllDiscounts(autoLoadNoneDiscount);
        }

        /// <summary>
        /// Gets the meal orders.
        /// </summary>
        /// <returns></returns>
        public static List<MealOrder> GetMealOrders()
        {
            return MealOrder.GetMealOrders();
        }

        /// <summary>
        /// Gets the meal order.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public static MealOrder GetMealOrder(long id)
        {
            return MealOrder.GetMealOrder(id);
        }

        /// <summary>
        /// Gets all meal order items.
        /// </summary>
        /// <returns></returns>
        public static List<MealOrderItem> GetAllMealOrderItems(long mealOrderId)
        {
            return MealOrderItem.GetMealOrderItems(mealOrderId);
        }

        /// <summary>
        /// Gets all menu items.
        /// </summary>
        /// <returns></returns>
        public static List<MenuItem> GetAllMenuItems()
        {
            return MenuItem.GetAllMenuItems();
        }

        /// <summary>
        /// Gets all taxes.
        /// </summary>
        /// <returns></returns>
        public static List<Tax> GetAllTaxes()
        {
            return Tax.GetAllTaxes();
        }

        /// <summary>
        /// Submits the order.
        /// </summary>
        /// <param name="mealOrder">The meal order.</param>
        /// <param name="mealOrderItems">The meal order items.</param>
        /// <param name="mealOrderTaxes">The meal order taxes.</param>
        /// <returns>
        /// The Id of the newly created meal order
        /// </returns>
        public static long SubmitOrder(MealOrder mealOrder, List<MealOrderItem> mealOrderItems, List<MealOrderTax> mealOrderTaxes)
        {
            long mealOrderId = MealOrder.SaveMealOrder(mealOrder);

            mealOrderItems.ForEach(mealOrderItem => mealOrderItem.MealOrderId = mealOrderId);
            MealOrderItem.SaveMealOrderItems(mealOrderItems);

            mealOrderTaxes.ForEach(mealOrderTax => mealOrderTax.MealOrderId = mealOrderId);
            MealOrderTax.SaveMealOrderTaxes(mealOrderTaxes);
            return mealOrderId;
        }

        /// <summary>
        /// Gets the discount.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public static Discount GetDiscount(long id)
        {
            return Discount.GetDiscount(id);
        }

        /// <summary>
        /// Gets the menu items.
        /// </summary>  
        /// <param name="mealOrderId">The meal order identifier.</param>
        /// <returns></returns>
        public static List<MenuItem> GetMenuItems(long mealOrderId)
        {
            return MenuItem.GetMenuItems(mealOrderId);
        }

        /// <summary>
        /// Gets the meal order taxes.
        /// </summary>
        /// <param name="mealOrderId">The meal order identifier.</param>
        /// <returns></returns>
        public static List<Tax> GetMealOrderTaxes(long mealOrderId)
        {
            return Tax.GetMealOrderTaxes(mealOrderId);
        }

        /// <summary>
        /// Saves the discount.
        /// </summary>
        /// <param name="discount">The discount.</param>
        public static void SaveDiscount(Discount discount)
        {
            Discount.SaveDiscount(discount);
        }

        /// <summary>
        /// Deletes the discount.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public static void DeleteDiscount(long id)
        {
            Discount.DeleteDiscount(id);
        }

        /// <summary>
        /// Gets all employees.
        /// </summary>
        /// <returns></returns>
        public static List<Employee> GetAllEmployees()
        {
            return Employee.GetAllEmployees();
        }

        /// <summary>
        /// Saves the employee.
        /// </summary>
        /// <param name="employee">The employee.</param> 
        /// <param name="updatePassword">if set to <c>true</c> [update password].</param>
        public static void SaveEmployee(Employee employee, bool updatePassword = false)
        {
            Employee.SaveEmployee(employee, updatePassword);
        }

        /// <summary>
        /// Deletes the employee.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public static void DeleteEmployee(long id)
        {
            Employee.DeleteEmployee(id);
        }

        /// <summary>
        /// Saves the tax.
        /// </summary>
        /// <param name="tax">The tax.</param>
        public static void SaveTax(Tax tax)
        {
            Tax.SaveTax(tax);
        }

        /// <summary>
        /// Deletes the tax.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public static void DeleteTax(long id)
        {
            Tax.DeleteTax(id);
        }

        /// <summary>
        /// Saves the menu item.
        /// </summary>
        /// <param name="menuItem">The menu item.</param>
        public static void SaveMenuItem(MenuItem menuItem)
        {
            MenuItem.SaveMenuItem(menuItem);            
        }

        /// <summary>
        /// Deletes the menu item.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public static void DeleteMenuItem(long id)
        {
            MenuItem.DeleteMenuItem(id);
        }
    }
}
