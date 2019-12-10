using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using USAbleLife.OrderManagement.Data.Exceptions;

namespace USAbleLife.OrderManagement.Data.DataAccess
{
    public enum DiscountType { FixedAmount = 0, Percentage = 1};

    public partial class Discount
    {
        /// <summary>
        /// Gets all discounts.
        /// </summary>
        /// <returns></returns>
        internal static List<Discount> GetAllDiscounts(bool autoLoadNoneDiscount = true)
        {
            List<Discount> discounts = new OrderManagementDataContext().Discounts.ToList();
            if (autoLoadNoneDiscount)
            {
                discounts.Insert(0, new Discount { Amount = 0, Id = -1, Name = "None" }); // Add the 'None' option
            }
            return discounts;
        }

        /// <summary>
        /// Gets the type of discount.
        /// </summary>
        /// <value>
        /// The type of discount.
        /// </value>
        public DiscountType TypeOfDiscount => (DiscountType)Type;

        /// <summary>
        /// Gets the discount by id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        internal static Discount GetDiscount(long id)
        {
            OrderManagementDataContext context = new OrderManagementDataContext();
            return context.Discounts.FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Gets the discount amount.
        /// </summary>
        /// <param name="mealOrderId">The meal order identifier.</param>
        /// <returns></returns>
        internal static decimal GetDiscountAmount(long mealOrderId)
        {
            MealOrder mealOrder = MealOrder.GetMealOrder(mealOrderId);
            if (mealOrder?.DiscountId == null)
            {
                return 0.0M;
            }

            Discount discount = GetDiscount(mealOrder.DiscountId.Value);
            return discount.TypeOfDiscount == DiscountType.FixedAmount ? discount.Amount : discount.Amount / 100 * mealOrder.SubTotal;
        }

        /// <summary>
        /// Saves the discount.
        /// </summary>
        /// <param name="discount">The discount.</param>
        internal static void SaveDiscount(Discount discount)
        {
            discount.ValidateDiscount();
            OrderManagementDataContext context = new OrderManagementDataContext();
            if (discount.Id <= 0)
            {
                context.Discounts.InsertOnSubmit(discount);
            }
            else
            {
                Discount oldDiscount = context.Discounts.FirstOrDefault(x => x.Id == discount.Id);
                if (oldDiscount == null)
                {
                    return;
                }

                oldDiscount.Amount = discount.Amount;
                oldDiscount.IsDeleted = discount.IsDeleted;
                oldDiscount.Name = discount.Name;
                oldDiscount.Type = discount.Type;
            }
            context.SubmitChanges();
        }

        /// <summary>
        /// Deletes the discount.
        /// </summary>
        /// <param name="id">The identifier.</param>
        internal static void DeleteDiscount(long id)
        {
            OrderManagementDataContext context = new OrderManagementDataContext();
            Discount discount = context.Discounts.FirstOrDefault(x => x.Id == id);
            if (discount != null)
            {
                discount.IsDeleted = true;
                context.SubmitChanges();
            }
        }

        /// <summary>
        /// Determines if the Discount Name is valid.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        internal static bool DiscountNameIsValid(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return false;
            }

            // Discount name must be alpha numeric and 1 to 25 characters
            Regex regex = new Regex("^[0-9a-zA-Z ]{1,25}$");
            return regex.Match(name).Success;
        }

        /// <summary>
        /// Discounts the name is unique.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        internal static bool DiscountNameIsUnique(string name, long id)
        {
            OrderManagementDataContext context = new OrderManagementDataContext();
            return !context.Discounts.Where(x => x.Id != id && x.IsDeleted == false).Any(x => x.Name == name);
        }

        /// <summary>
        /// Determines if the discount amount is valid
        /// </summary>
        /// <param name="amount">The amount.</param>
        /// <returns></returns>
        internal static bool DiscountAmountIsValid(decimal amount)
        {
            return BitConverter.GetBytes(decimal.GetBits(amount)[3])[2] <= 2;
        }
    }

    internal static class DiscountExtensions
    {
        internal static void ValidateDiscount(this Discount discount, bool checkNameUniqueness = true)
        {
            if (!Discount.DiscountNameIsValid(discount.Name))
            {
                throw new DiscountNameException("Discount name must be alpha numeric and 1 to 25 characters");
            }

            if (checkNameUniqueness && !Discount.DiscountNameIsUnique(discount.Name, discount.Id))
            {
                throw new DiscountNameException("Discount name must be unique");
            }

            if (discount.TypeOfDiscount == DiscountType.FixedAmount && (discount.Amount < 0 || discount.Amount > 100) && Discount.DiscountAmountIsValid(discount.Amount))
            {
                throw new DiscountAmountException("Discount amount for fixed discounts must be between 0 and 100 with up to two decimals");
            }

            if (discount.TypeOfDiscount == DiscountType.Percentage && (discount.Amount < 1 || discount.Amount > 100))
            {
                throw new DiscountAmountException("Discount amount for percents must be between 1 and 100");
            }
        }
    }
}
