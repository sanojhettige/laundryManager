using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LaundryManagerWeb.App_Code
{
    public class GlobalFunctions
    {
        public static SelectList MesureTypes(int selectedType)
        {
            return new SelectList(new[]
            {
                new { ID = 1, Name = "Kg" },
                new { ID = 2, Name = "Unit" },
            },
            "ID", "Name", selectedType);
        }

        public static SelectList UserRoles()
        {
            return new SelectList(new[]
            {
                new { ID = "Admin", Name = "Admin" },
                new { ID = "Customer", Name = "Customer" },
            },
            "ID", "Name", 1);
        }

        public static string OrderStatus(int id)
        {
            var status = "New";

            if (id == 1)
            {
                status = "Accepted";
            }
            else if (id == 2)
            {
                status = "Processing";
            }
            else if (id == 3)
            {
                status = "Ready to Dispatch";
            }
            else if (id == 4)
            {
                status = "Dispatched";
            }
            else if (id == 5)
            {
                status = "Customer Collected";
            }
            else if (id == 6)
            {
                status = "Cancelled";
            }
            else
            {
                status = "New";
            }
            return status;
        }
    }
}