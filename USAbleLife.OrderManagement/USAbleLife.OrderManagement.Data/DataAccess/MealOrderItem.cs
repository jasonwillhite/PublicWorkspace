using System.Collections.Generic;
using System.Linq;
namespace USAbleLife.OrderManagement.Data.DataAccess
{
    public partial class MealOrderItem
    {
        /// <summary>
        /// Gets the meal order items.
        /// </summary>
        /// <param name="mealOrderId">The meal order identifier.</param>
        /// <returns></returns>
        public static List<MealOrderItem> GetMealOrderItems(long mealOrderId)
        {
            return new OrderManagementDataContext().MealOrderItems.Where(mealOrderItem => mealOrderItem.MealOrderId == mealOrderId).ToList();
        }

        /// <summary>
        /// Saves the meal order items.
        /// </summary>
        /// <param name="mealOrderItems">The meal order items.</param>
        public static void SaveMealOrderItems(List<MealOrderItem> mealOrderItems)
        {
            OrderManagementDataContext context = new OrderManagementDataContext();
            context.MealOrderItems.InsertAllOnSubmit(mealOrderItems);
            context.SubmitChanges();            
        }
    }
}
