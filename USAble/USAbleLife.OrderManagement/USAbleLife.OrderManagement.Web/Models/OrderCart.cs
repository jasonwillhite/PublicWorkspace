using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using USAbleLife.OrderManagement.Data.DataAccess;

namespace USAbleLife.OrderManagement.Web.Models
{
    [Serializable]
    public class OrderCart
    {
        public OrderCart()
        {
            Parallel.Invoke
            (
                () => { AvailableDiscounts = Data.Api.Order.GetAllDiscounts(); },
                () => { AvailableMenuItems = Data.Api.Order.GetAllMenuItems(); },
                () => { AvailableTaxes = Data.Api.Order.GetAllTaxes(); }
            );
        }

        private List<MenuItem> _AvailableMenuItems = new List<MenuItem>();
        /// <summary>
        /// Gets or sets the available menu items.
        /// </summary>
        /// <value>
        /// The available menu items.
        /// </value>
        public List<MenuItem> AvailableMenuItems
        {
            get { return _AvailableMenuItems; }
            set { _AvailableMenuItems = value; }
        }

        private List<Discount> _AvailableDiscounts = new List<Discount>();
        /// <summary>
        /// Gets or sets the available discounts.
        /// </summary>
        /// <value>
        /// The available discounts.
        /// </value>
        public List<Discount> AvailableDiscounts
        {
            get { return _AvailableDiscounts; }
            set { _AvailableDiscounts = value; }
        }

        private List<Tax> _AvailableTaxes = new List<Tax>();
        /// <summary>
        /// Gets or sets the available taxes.
        /// </summary>
        /// <value>
        /// The available taxes.
        /// </value>
        public List<Tax> AvailableTaxes
        {
            get { return _AvailableTaxes; }
            set { _AvailableTaxes = value; }
        }

        private List<MenuItem> _SelectedMenuItems = new List<MenuItem>();
        /// <summary>
        /// Gets or sets the selected menu items.
        /// </summary>
        /// <value>
        /// The selected menu items.
        /// </value>
        public List<MenuItem> SelectedMenuItems
        {
            get { return _SelectedMenuItems; }
            set { _SelectedMenuItems = value; }
        }

        private HashSet<Tax> _SelectedTaxes = new HashSet<Tax>();
        /// <summary>
        /// Gets or sets the selected taxes.
        /// </summary>
        /// <value>
        /// The selected taxes.
        /// </value>
        public HashSet<Tax> SelectedTaxes
        {
            get { return _SelectedTaxes; }
            set { _SelectedTaxes = value; }
        }

        /// <summary>
        /// Gets or sets the selected discount.
        /// </summary>
        /// <value>
        /// The selected discount.
        /// </value>
        public Discount SelectedDiscount { get; set; }

        private bool _CanSubmit = true;
        /// <summary>
        /// Gets or sets a value indicating whether this instance can sumbit.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance can sumbit; otherwise, <c>false</c>.
        /// </value>
        public bool CanSumbit
        {
            get { return _CanSubmit; }
            set { _CanSubmit = value; }
        }
    }
}