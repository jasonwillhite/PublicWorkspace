using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using USAbleLife.OrderManagement.Data.Exceptions;

namespace USAbleLife.OrderManagement.Data.DataAccess
{
    public partial class Tax
    {
        /// <summary>
        /// Gets all taxes.
        /// </summary>
        /// <returns></returns>
        internal static List<Tax> GetAllTaxes()
        {
            return new OrderManagementDataContext().Taxes.Where(x => !x.IsDeleted ?? true).ToList();
        }

        /// <summary>
        /// Gets the meal order taxes.
        /// </summary>
        /// <param name="mealOrderId">The meal order identifier.</param>
        /// <returns></returns>
        internal static List<Tax> GetMealOrderTaxes(long mealOrderId)
        {
            OrderManagementDataContext context = new OrderManagementDataContext();
            return context.ExecuteQuery<Tax>("Select * from Tax where Id in (Select TaxId from MealOrderTax where MealOrderId = {0})", mealOrderId).ToList();
        }

        /// <summary>
        /// Gets the total tax.
        /// </summary>
        /// <param name="mealOrderId">The meal order identifier.</param>
        /// <returns></returns>
        internal static decimal GetTotalTax(long mealOrderId)
        {
            return GetMealOrderTaxes(mealOrderId).Sum(x => x.Percentage);
        }

        /// <summary>
        /// Determines if the tax name is valid
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        internal static bool TaxNameIsValid(string name)
        {
            // Tax name must be alpha numeric and 1 to 25 characters
            return new Regex("^[0-9a-zA-Z ]{1,25}$").Match(name).Success;
        }

        /// <summary>
        /// Determines if the tax name is unique
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        internal static bool TaxNameIsUnique(string name, long id)
        {
            OrderManagementDataContext context = new OrderManagementDataContext();
            return !context.Taxes.Where(x => x.Id != id).Where(x => !x.IsDeleted ?? true).Any(x => x.Name == name);
        }

        /// <summary>
        /// Saves the tax.
        /// </summary>
        /// <param name="tax">The tax.</param>
        internal static void SaveTax(Tax tax)
        {
            tax.ValidateTax();
            OrderManagementDataContext context = new OrderManagementDataContext();
            if (tax.Id <= 0)
            {
                context.Taxes.InsertOnSubmit(tax);
            }
            else
            {
                Tax existingTax = context.Taxes.FirstOrDefault(x => x.Id == tax.Id);
                if (existingTax != null)
                {
                    existingTax.Name = tax.Name;
                    existingTax.Percentage = tax.Percentage;
                }
            }
            context.SubmitChanges();
        }

        /// <summary>
        /// Deletes the tax.
        /// </summary>
        /// <param name="id">The identifier.</param>
        internal static void DeleteTax(long id)
        {
            OrderManagementDataContext context = new OrderManagementDataContext();
            Tax tax = context.Taxes.FirstOrDefault(x => x.Id == id);
            if (tax != null)
            {
                tax.IsDeleted = true;
                context.SubmitChanges();
            }
        }
    }

    internal static class TaxExtensions
    {
        /// <summary>
        /// Validates the tax.
        /// </summary>
        /// <param name="tax">The tax.</param>
        /// <exception cref="TaxPercentageException">Tax percentage must be between 1 and 100</exception>
        /// <exception cref="TaxNameException">
        /// Tax name must be unique
        /// or
        /// Tax name must alpha numeric and 1 to 25 characters
        /// </exception>
        internal static void ValidateTax(this Tax tax)
        {
            if (tax.Percentage < 1 || tax.Percentage > 100)
            {
                throw new TaxPercentageException("Tax percentage must be between 1 and 100");
            }

            if (!Tax.TaxNameIsUnique(tax.Name, tax.Id))
            {
                throw new TaxNameException("Tax name must be unique");
            }

            if (!Tax.TaxNameIsValid(tax.Name))
            {
                throw new TaxNameException("Tax name must alpha numeric and 1 to 25 characters");
            }
        }
    }
}
