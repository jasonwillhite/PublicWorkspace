using System.Collections.Generic;

namespace USAbleLife.OrderManagement.Data.DataAccess
{
    public partial class MealOrderTax
    {
        /// <summary>
        /// Saves the meal order taxes.
        /// </summary>
        /// <param name="mealOrderTaxes">The meal order taxes.</param>
        internal static void SaveMealOrderTaxes(List<MealOrderTax> mealOrderTaxes)
        {
            OrderManagementDataContext context = new OrderManagementDataContext();
            context.MealOrderTaxes.InsertAllOnSubmit(mealOrderTaxes);
            context.SubmitChanges();
        }
    }
}
