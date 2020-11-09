using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Globalization;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using LaundryManagerWeb.Models;
using AutoMapper;
using LaundryManagerWeb.Services;
using LaundryManagerWeb.App_Code;
using LaundryManagerWeb.ViewModels;

namespace LaundryManagerWeb.Controllers
{
    public class CartController : Controller
    {
        #region Fields

        private ApplicationUserManager _userManager;
        private ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private string userId;

        #endregion

        #region Constructor

        public CartController()
        {
            _context = new ApplicationDbContext();
            userId = "7hjsdjfj"; // User.Identity.GetUserId();

        }

        #endregion

        #region Methods

        // GET: /Cart/
        public ActionResult Index()
        {
            var products = _context.Category?.ToList();
            var cartItems = new List<CartItemModel>();
            if (HttpContext.Session["cartItems"] != null)
                cartItems = (List<CartItemModel>)HttpContext.Session["cartItems"];


            var viewModel = new OrderFormViewModel
            {
                Order = new Order(),
                Category = products,
                CartItem = cartItems.ToArray(),
            };

            var list = GlobalFunctions.MesureTypes(0);

            ViewData["MesureTypes"] = list;
 
            return View("Index", viewModel);
        }

        public static bool Contains(Array a, object val)
        {
            return Array.IndexOf(a, val) != -1;
        }

        // POST: /Cart/Add
        [HttpGet]
        //  [ValidateAntiForgeryToken]
        public ActionResult Add(int id)
        {
            if (id <= 0)
                return RedirectToAction("Index");

            // check if product exist


            var selectedItem = _context.Category.SingleOrDefault(c => c.Id == id);
            if (selectedItem == null)
                return RedirectToAction("Index");

            // check if there are already cart instance
            var cartItems = new List<CartItemModel>();

            if (HttpContext.Session["cartItems"] != null)
                cartItems = (List<CartItemModel>)HttpContext.Session["cartItems"];

                // check if the item are already in the cart
                // if the item is already in the cart,
                // increase the quantity by 1
                if (cartItems.Exists(x => x.Id == selectedItem.Id))
            {
                cartItems.Find(x => x.Id == selectedItem.Id).Quantity++;
                HttpContext.Session.Add("cartItems", cartItems);
            }
            else
            {
                var item = new CartItemModel
                {
                    Id = selectedItem.Id,
                    Name = selectedItem.Name,
                    Price = selectedItem.UnitCharge,
                    UnitType = selectedItem.UnitType == 1 ? "Kg" : "Pieces",
                    Quantity = 1,
                };

           
                cartItems.Add(item);

                HttpContext.Session.Add("cartItems", cartItems);
            }

            return RedirectToAction("Index");
        }



        // POST: /Cart/Remove
        [HttpGet]
        //  [ValidateAntiForgeryToken]
        public ActionResult Remove(int Id)
        {
            if (Id <= 0)
                return RedirectToAction("Index");


            // check if there are already cart instance
            var cartItems = new List<CartItemModel>();

            if (HttpContext.Session["cartItems"] != null)
                cartItems = (List<CartItemModel>)HttpContext.Session["cartItems"];

            if (cartItems.Exists(x => x.Id == Id))
            {
                cartItems.RemoveAll(x => x.Id == Id);
                HttpContext.Session.Add("cartItems", cartItems);
            }

            return RedirectToAction("Index");
        }


        // GET: /Cart/Checkout
        [Authorize]
        public async Task<ActionResult> Checkout()
        {
            if (HttpContext.Session["cartItems"] == null)
                return View("Index");

            var cartItems = new List<CartItemModel>();

            if (HttpContext.Session["cartItems"] != null)
                cartItems = (List<CartItemModel>)HttpContext.Session["cartItems"];


            var viewModel = new OrderFormViewModel
            {
                Order = new Order(),
                CartItem = cartItems.ToArray(),
            };


            return View(viewModel);
        }

        // POST: /Cart/Checkout
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> Checkout(Order order)
        {
            var totalOrderPrice = 0m;


            if (!ModelState.IsValid)
            {
                var cartItems = new List<CartItemModel>();

                if (HttpContext.Session["cartItems"] != null)
                    cartItems = (List<CartItemModel>)HttpContext.Session["cartItems"];


                var viewModel = new OrderFormViewModel
                {
                    Order = new Order(),
                    CartItem = cartItems.ToArray(),
                };

                return View("Checkout", viewModel);
            }
            try
            {
                var orderItemEntities = new List<OrderItem>();
                var cartItems = new List<CartItemModel>();


                // get cart session
                if (HttpContext.Session["cartItems"] != null)
                    cartItems = (List<CartItemModel>)HttpContext.Session["cartItems"];


                foreach (var item in cartItems)
                {
                    var currentItem = _context.Category.SingleOrDefault(c => c.Id == item.Id);
                    if (currentItem != null)
                    {
                        var newOrderItem = new OrderItem
                        {
                            OrderId = order.Id,
                            ProductId = item.Id,
                            Quantity = item.Quantity,
                            UnitPrice = item.Price,
                            TotalPrice = (item.Price * item.Quantity)
                        };

                        orderItemEntities.Add(newOrderItem);
                        totalOrderPrice += newOrderItem.TotalPrice;
                    }
                }


                if (order.Id == 0)
                {
                    order.CreatedAt = DateTime.Now;
                    order.ModifiedAt = DateTime.Now;
                    order.CreatedBy = userId;
                    order.ModifiedBy = userId;

                    // check if the order have item/s
                    if (orderItemEntities.Count > 0)
                    {
                        order.Items = orderItemEntities;
                        order.TotalCost = totalOrderPrice;
                        order.TotalDiscount = 0;

                        // save
                        _context.Order.Add(order);

                        // clear cart session
                        HttpContext.Session.Remove("cartItems");

                        return RedirectToAction("Index", "Order");
                    }
                }
                else
                {
                    var selectedOrder = _context.Order.Single(m => m.Id == order.Id);
                    selectedOrder.CustomerName = order.CustomerName;
                    selectedOrder.CustomerPhone = order.CustomerPhone;
                    selectedOrder.PickUpPerson = order.PickUpPerson;
                    selectedOrder.PickUpPersonPhone = order.PickUpPersonPhone;
                    selectedOrder.PickUpDateTime = order.PickUpDateTime;
                    selectedOrder.ModifiedAt = DateTime.Now;
                    selectedOrder.ModifiedBy = userId;

                    _context.SaveChanges();

                }

                return RedirectToAction("index", "Order");

            }
            catch (Exception e)
            {
                return RedirectToAction("Checkout", "Cart");
            }
        }

        #endregion

        #region Helpers

        private string GenerateUniqueOrderNumber()
        {
            var rand = new Random();
            string orderNumber = rand.Next(100, 999) + "-" + rand.Next(100, 999) + "-" + rand.Next(100000, 999999);

            var orderId = orderNumber;

            while (orderId != null)
            {
                orderNumber = rand.Next(100, 999) + "-" + rand.Next(100, 999) + "-" + rand.Next(100000, 999999);
                orderId = orderNumber;
            }

            return orderNumber;
        }

        #endregion
    }
}