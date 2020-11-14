using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LaundryManagerWeb.Models;
using LaundryManagerWeb.ViewModels;
using Microsoft.AspNet.Identity;
using LaundryManagerWeb.App_Code;

namespace LaundryManagerWeb.Controllers
{
    public class ReportController : Controller
    {
        private ApplicationDbContext _context;
        private string userId;


        public ReportController()
        {
            _context = new ApplicationDbContext();
            userId = ""; // User.Identity.GetUserId();

        }

        // GET: Order/Random 

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Report
        public ActionResult Index()
        {
            return View("Orders");
        }

        // GET: Orders
        public ActionResult Orders()
        {
            return View();
        }

        // GET: Customer
        public ActionResult Customers()
        {
            return View();
        }

        // GET: Payments
        public ActionResult DuePayments()
        {
            return View();
        }

    }
}