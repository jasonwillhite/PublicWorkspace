using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using USAbleLife.OrderManagement.Data.Exceptions;

namespace USAbleLife.OrderManagement.Data.DataAccess
{
    public partial class MenuItem
    {
        /// <summary>
        /// Gets all menu items.
        /// </summary>
        /// <returns></returns>
        internal static List<MenuItem> GetAllMenuItems()
        {
            return new OrderManagementDataContext().MenuItems.Where(x => !x.IsDeleted ?? true).ToList();
        }

        /// <summary>
        /// Gets the menu items.
        /// Definitely given more time, this would need to be redone. This is done like this so that the OrderCart view model can be re-used like it
        /// is when an order is being put together. This is not optimal though
        /// </summary>
        /// <param name="mealOrderId">The meal order identifier.</param>
        /// <returns></returns>
        internal static List<MenuItem> GetMenuItems(long mealOrderId)
        {
            OrderManagementDataContext context = new OrderManagementDataContext();
            List<MenuItem> menuItems = new List<MenuItem>();
            foreach (var mealOrderMenuItem in from x in context.MealOrderItems where x.MealOrderId == mealOrderId select x)
            {
                for (int i = 0; i < mealOrderMenuItem.Quantity; i++)
                {
                    menuItems.Add(context.MenuItems.FirstOrDefault(x => x.Id == mealOrderMenuItem.MenuItemId));
                }
            }
            return menuItems;
        }

        /// <summary>
        /// Saves the menu item.
        /// </summary>
        /// <param name="menuItem">The menu item.</param>
        internal static void SaveMenuItem(MenuItem menuItem)
        {
            menuItem.ValidateMenuItem();
            OrderManagementDataContext context = new OrderManagementDataContext();
            if (menuItem.Id <= 0)
            {
                context.MenuItems.InsertOnSubmit(menuItem);
            }
            else
            {
                MenuItem existingMenuItem = context.MenuItems.FirstOrDefault(x => x.Id == menuItem.Id);
                if (existingMenuItem != null)
                {
                    existingMenuItem.Name = menuItem.Name;
                    existingMenuItem.Price = menuItem.Price;
                }
            }
            context.SubmitChanges();
        }

        /// <summary>
        /// Determines if the menu item name is valid
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        internal static bool MenuItemNameIsValid(string name)
        {
            // MenuItem name must be alpha numeric and 1 to 25 characters
            Regex regex = new Regex("^[0-9a-zA-Z ]{1,25}$");
            return !string.IsNullOrEmpty(name) && regex.Match(name).Success;
        }

        /// <summary>
        /// Determines if the menu item is unique
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        internal static bool MenuItemIsUnique(string name, long id)
        {
            OrderManagementDataContext context = new OrderManagementDataContext();
            return !context.MenuItems.Where(x => x.Id != id).Where(x => !x.IsDeleted ?? true).Any(x => x.Name == name);
        }

        /// <summary>
        /// Determines if the menu item price is valid
        /// </summary>
        /// <param name="price">The price.</param>
        /// <returns></returns>
        internal static bool MenuItemPriceIsValid(decimal price)
        {
            return new Regex(@"^\d{0,8}(\.\d{1,4})?$").Match(price.ToString(CultureInfo.InvariantCulture)).Success;
        }

        /// <summary>
        /// Deletes the menu item.
        /// </summary>
        /// <param name="id">The identifier.</param>
        internal static void DeleteMenuItem(long id)
        {
            OrderManagementDataContext context = new OrderManagementDataContext();
            MenuItem menuItem = context.MenuItems.FirstOrDefault(x => x.Id == id);
            if (menuItem != null)
            {
                menuItem.IsDeleted = true;
                context.SubmitChanges();
            }
        }
    }

    internal static class MenuItemExtensions
    {
        /// <summary>
        /// Validates the menu item.
        /// </summary>
        /// <param name="menuItem">The menu item.</param>
        /// <exception cref="MenuItemNameException">
        /// Menu Item name must be alpha numeric and 1 to 25 characters
        /// or
        /// Menu Item name must be unique
        /// </exception>
        /// <exception cref="MenuItemPriceException">Menu Item price must be 0 to 100 and have no more than 2 decimal places</exception>
        internal static void ValidateMenuItem(this MenuItem menuItem)
        {
            if (!MenuItem.MenuItemNameIsValid(menuItem.Name))
            {
                throw new MenuItemNameException("Menu Item name must be alpha numeric and 1 to 25 characters");
            }   

            if (!MenuItem.MenuItemIsUnique(menuItem.Name, menuItem.Id))
            {
                throw new MenuItemNameException("Menu Item name must be unique");
            }

            if (!MenuItem.MenuItemPriceIsValid(menuItem.Price))
            {
                throw new MenuItemPriceException("Menu Item price must be 0 to 100 and have no more than 2 decimal places");
            }
        }
    }
}
