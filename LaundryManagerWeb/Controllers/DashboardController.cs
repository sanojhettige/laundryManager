using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using LaundryManagerWeb.Models;

namespace LaundryManagerWeb.Controllers
{
    public class DashboardController : Controller
    {
        private ApplicationDbContext _context;
        public DashboardController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Dashboard
        public ActionResult Index()
        {
            if (User.IsInRole(RoleName.Customer)) {
                return RedirectToAction("Index", "Home");
            }
                int totalRevenue = (int)_context.Order.Sum(m => m.PaidAmount);
            int totalOrders = _context.Order.Count();
            int pendingOrders = _context.Order.Where(o => o.Status == 0).Count();
            int deliveredOrders = _context.Order.Where(o => o.Status == 5).Count();
            int totalCustomers = _context.Order.GroupBy(o => o.CustomerName).Count();

            ViewData["totalRevenue"] = totalRevenue.ToString();
            ViewData["totalOrders"] = totalOrders.ToString();
            ViewData["pendingOrders"] = pendingOrders.ToString();
            ViewData["deliveredOrders"] = deliveredOrders.ToString();
            ViewData["totalCustomers"] = totalCustomers.ToString();

            return View();
        }
    }
}