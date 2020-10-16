using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LaundryManagerWeb.Models;

namespace LaundryManagerWeb.ViewModels
{
    public class CategoryFormViewModel
    {
        public Category Category { get; set; }
        public string Title
        {
            get
            {
                if (Category != null && Category.Id != 0)


                    return "Edit Category";

                return "New Category";
            }
        }
    }
}