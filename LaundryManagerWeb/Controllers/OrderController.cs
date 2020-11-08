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

        // GET POST: Create
        public ActionResult Create()
        {
            var products = _context.Category?.ToList();
            var viewModel = new OrderFormViewModel
            {
                Order = new Order(),
                Category = products
            };

            var list = GlobalFunctions.MesureTypes(0);
            ViewData["MesureTypes"] = list;

            if (User.IsInRole(RoleName.Admin))
            {
                return View("Create", viewModel);

            } else if(User.IsInRole(RoleName.Customer))
            {
                return View("Create", viewModel);
            } else
            {
                return View("../Account/Login");
            }
        }

        // POST: Save
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.Customer)]
        public ActionResult Save(Order order)
        {
            var products = _context.Category.ToList();
            //Model.State to check validation from the model
            if (!ModelState.IsValid)
            {
                var viewModel = new OrderFormViewModel
                {
                    Order = order,
                    Category = products
                };

                return View("Create", viewModel);
            }
            try
            {
                if (order.Id == 0)
                {
                    order.CreatedAt = DateTime.Now;
                    order.ModifiedAt = DateTime.Now;
                    order.CreatedBy = userId;
                    order.ModifiedBy = userId;
                    _context.Order.Add(order);

                }
                else
                {
                    var selectedOrder = _context.Order.Single(m => m.Id == order.Id);
                    selectedOrder.ModifiedAt = DateTime.Now;
                    selectedOrder.ModifiedBy = userId;

                }

                _context.SaveChanges();

                return RedirectToAction("ViewOrder", "Order");

            }
            catch (Exception e)
            {
                return RedirectToAction("ViewOrder", "Order");
            }

        }

        // GET: ViewOrder
        public ActionResult ViewOrder(int id)
        {
            var order = _context.Order.SingleOrDefault(c => c.Id == id);
            var products = _context.Category.ToList();
            if (order == null)
                return HttpNotFound();

            var viewModel = new OrderFormViewModel
            {
                Order = order,
                Category = products

            };

            if (User.IsInRole(RoleName.Admin))
            {
                return View("ViewOrder", viewModel);

            }
            else if (User.IsInRole(RoleName.Customer))
            {
                return View("ViewOrder", viewModel);
            }
            else
            {
                return View("../Account/Login");
            }
        }
    }
}