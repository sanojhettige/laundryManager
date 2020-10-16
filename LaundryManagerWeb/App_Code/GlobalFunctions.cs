using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LaundryManagerWeb.App_Code
{
    public class GlobalFunctions
    {
        public static SelectList MesureTypes()
        {
            return new SelectList(new[]
            {
                new { ID = 1, Name = "Kg" },
                new { ID = 2, Name = "Unit" },
            },
            "ID", "Name", 1);
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
    }
}