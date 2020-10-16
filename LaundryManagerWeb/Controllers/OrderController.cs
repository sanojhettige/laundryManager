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
    public class OrderController : Controller
    {
        private ApplicationDbContext _context;
        private string userId;


        public OrderController()
        {
            _context = new ApplicationDbContext();
            userId = ""; // User.Identity.GetUserId();

        }

        // GET: Order/Random 

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }


        // GET: Order
        public ActionResult Index()
        {
            return View();
        }


        // GET: Create
        public ActionResult Create()
        {
            if (User.IsInRole(RoleName.Admin))
            {
                return View();

            } else if(User.IsInRole(RoleName.Customer))
            {
                return View();
            } else
            {
                return View("../Account/Login");
            }
        }
    }
}