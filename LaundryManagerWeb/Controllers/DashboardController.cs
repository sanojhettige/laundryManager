using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using LaundryManagerWeb.Models;

namespace ApartmentManager.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            int propTotal = 0;
            int aptTotal = 0;
            int owners = 0;
            int tenants = 0;
            int totalDue = 0;
            int totalVacant = 0;
            ViewData["propTotal"] = propTotal.ToString();
            ViewData["aptTotal"] = aptTotal.ToString();
            ViewData["ownerTotal"] = owners.ToString();
            ViewData["tenantTotal"] = tenants.ToString();
            ViewData["totalDue"] = totalDue.ToString();
            ViewData["totalVacant"] = totalVacant.ToString();

            return View();
        }
    }
}