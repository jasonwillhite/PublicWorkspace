using System.Collections.Generic;
using System.Linq;

namespace USAbleLife.OrderManagement.Data.DataAccess
{
    public partial class MealOrder
    {
        /// <summary>
        /// Gets the meal orders.
        /// </summary>
        /// <returns></returns>
        internal static List<MealOrder> GetMealOrders()
        {
            return new OrderManagementDataContext().MealOrders.ToList();
        }

        /// <summary>
        /// Saves the meal order.
        /// </summary>
        /// <param name="mealOrder">The meal order.</param>
        /// <returns></returns>
        internal static long SaveMealOrder(MealOrder mealOrder)
        {
            OrderManagementDataContext context = new OrderManagementDataContext();
            context.MealOrders.InsertOnSubmit(mealOrder);
            context.SubmitChanges();
            return mealOrder.Id;
        }

        /// <summary>
        /// Gets the name of the server.
        /// </summary>
        /// <value>
        /// The name of the server.
        /// </value>
        public string ServerName => EmployeeId > 0 ? Employee.GetEmployee(EmployeeId).ToString() : string.Empty;

        /// <summary>
        /// Gets the total.
        /// </summary>
        /// <value>
        /// The total.
        /// </value>
        public decimal Total => SubTotal - TotalDiscount + TotalTax;

        /// <summary>
        /// Gets the pre tax total.
        /// </summary>
        /// <value>
        /// The pre tax total.
        /// </value>
        public decimal PreTaxTotal => SubTotal - TotalDiscount;

        /// <summary>
        /// Gets the sub total.
        /// </summary>
        /// <value>
        /// The sub total.
        /// </value>
        public decimal SubTotal => MenuItem.GetMenuItems(Id).Sum(x => x.Price);
        
        /// <summary>
        /// Gets the total discount.
        /// </summary>
        /// <value>
        /// The total discount.
        /// </value>
        public decimal TotalDiscount => Discount.GetDiscountAmount(Id);

        /// <summary>
        /// Gets the total tax.
        /// </summary>
        /// <value>
        /// The total tax.
        /// </value>
        public decimal TotalTax => PreTaxTotal * Tax.GetTotalTax(Id) / 100;

        /// <summary>
        /// Gets the meal order.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public static MealOrder GetMealOrder(long id)
        {
            OrderManagementDataContext context = new OrderManagementDataContext();
            return context.MealOrders.FirstOrDefault(x => x.Id == id);
        }
    }
}