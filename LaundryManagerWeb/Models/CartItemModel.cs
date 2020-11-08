using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace LaundryManagerWeb.Models
{
    public class CartItemModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public decimal OldPrice { get; set; }

        public int Quantity { get; set; }

    }
}