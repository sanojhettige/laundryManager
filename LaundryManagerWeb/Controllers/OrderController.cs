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
            var LayOut = "~/Views/Shared/_Layout.cshtml";

            if (User.IsInRole(RoleName.Customer))
            {
                LayOut = "~/Views/Shared/_FE_Layout.cshtml";
            }

            ViewData["LayOut"] = LayOut;

            if (User.IsInRole(RoleName.Admin))
            {
                return View("AdminOrderList");
            } else if(User.IsInRole(RoleName.Customer))
            {
                return View();
            }
            else
            {
                return View("../Account/Login");
            }
        }

        // GET: Track Order
        public ActionResult TrackOrder(int id)
        {
            var order = _context.Order.SingleOrDefault(c => c.Id == id);
            var items = _context.OrderItem.Where(c => c.OrderId == id);
            if (order == null)
                return HttpNotFound();

            var viewModel = new OrderFormViewModel
            {
                Order = order,
                OrderItem = items

            };

            return View("TrackOrder", viewModel);
        }

        // GET: ViewOrder
        public ActionResult ViewOrder(int id)
        {
            var order = _context.Order.SingleOrDefault(c => c.Id == id);
            var items = _context.OrderItem.Where(c => c.OrderId == id);
            if (order == null)
                return HttpNotFound();

            var viewModel = new OrderFormViewModel
            {
                Order = order,
                OrderItem = items

            };

            if (User.IsInRole(RoleName.Admin) || User.IsInRole(RoleName.Customer))
            {
                var LayOut = "~/Views/Shared/_Layout.cshtml";

                if (User.IsInRole(RoleName.Customer))
                {
                    LayOut = "~/Views/Shared/_FE_Layout.cshtml";
                }

                ViewData["LayOut"] = LayOut;


                return View("ViewOrder", viewModel);
            }
            else
            {
                return View("../Account/Login");
            }
        }


        // GET: Create Payment
        [Authorize(Roles = RoleName.Admin)]
        public ActionResult Payment(int id)
        {
            var order = _context.Order.SingleOrDefault(c => c.Id == id);

            if (order == null)
                return HttpNotFound();


            var viewModel = new OrderFormViewModel
            {
                Order = order
            };

            return View("PaymentForm", viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.Admin)]
        public ActionResult SavePayment(Order order)
        {
            try
            {
                var selectedOrder = _context.Order.Single(m => m.Id == order.Id);
                selectedOrder.PaidAmount = order.PaidAmount;
                selectedOrder.PaidNote = order.PaidNote;
                selectedOrder.ModifiedAt = DateTime.Now;
                selectedOrder.ModifiedBy = userId;

                _context.SaveChanges();

                return RedirectToAction("index", "Order");

            }
            catch (Exception e)
            {
                return RedirectToAction("index", "Order");
            }
        }
    }
}