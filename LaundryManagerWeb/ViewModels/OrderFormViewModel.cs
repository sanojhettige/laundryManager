using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LaundryManagerWeb.Models;

namespace LaundryManagerWeb.ViewModels
{
    public class OrderFormViewModel
    {
        public Order Order { get; set; }

        public IEnumerable<Category> Category { get; set; }

        public IEnumerable<CartItemModel> CartItem { get; set; }

        public IEnumerable<OrderItem> OrderItem { get; set; }
        public string Title
        {
            get
            {
                if (Order != null && Order.Id != 0)


                    return "Edit Order";

                return "New Order";
            }
        }
    }
}