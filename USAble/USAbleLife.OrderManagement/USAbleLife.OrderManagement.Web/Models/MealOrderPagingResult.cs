using System;
using System.Collections.Generic;
using USAbleLife.OrderManagement.Data.DataAccess;

namespace USAbleLife.OrderManagement.Web.Models
{
    public class MealOrderPagingResult
    {
        public int TotalNumberOfMealOrders { get; set; }

        public int PageSize { get; set; }

        public int CurrentPage { get; set; }

        public int TotalNumberOfPages => (int)Math.Ceiling((decimal)TotalNumberOfMealOrders / PageSize);

        public List<MealOrder> MealOrders { get; set; }
    }
}