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
        //private readonly IMapper _mapper;

        private ISession Session => _httpContextAccessor.HttpContext.Session;
        private readonly string _cartItesmSessionKey = "CartItems";
        private readonly string _cartItemsCountSessionKey = "CartItemsCount";


        #endregion

        #region Constructor

        public CartController()
        {
            _context = new ApplicationDbContext();

        }

        public CartController(ApplicationUserManager userManager)
        {
            _userManager = userManager;

        }

        #endregion

        #region Methods

        // GET: /Cart/
        public ActionResult Index()
        {
            var products = _context.Category?.ToList();
            var viewModel = new OrderFormViewModel
            {
                Order = new Order(),
                Category = products
            };

            var list = GlobalFunctions.MesureTypes(0);

            var cartItems = new List<CartItemModel>();
            if (HttpContext.Session["cartItems"] != null)
                cartItems = (List<CartItemModel>)HttpContext.Session["cartItems"];

            ViewData["MesureTypes"] = list;
            ViewData["cartItems"] = cartItems;

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
            // if (Session.GetString(_cartItesmSessionKey) == null)
            //   return View("Index");

            var user = await GetCurrentUserAsync();
            var checkoutModel = new CheckoutModel();
            //var cartItems = JsonConvert.DeserializeObject<List<CartItemModel>>(Session.GetString(_cartItesmSessionKey));

            // checkoutModel.CartItemModel = cartItems;

            return View(checkoutModel);
        }

        // POST: /Cart/Checkout
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> Checkout(CheckoutModel model)
        {
            // get current user
            var user = await GetCurrentUserAsync();
            var totalOrderPrice = 0m;

            // create order entity 
            var orderEntity = new Order
            {
                OrderReference = GenerateUniqueOrderNumber(),
                UserId = user.Id,
                Status = 0,
                CustomerName = "",
                CustomerPhone = "",
                PickUpPerson = "",
                PickUpDateTime = DateTime.Now,
                PickUpPersonPhone = ""
            };

            var orderItemEntities = new List<OrderItem>();
            var cartItems = new List<CartItemModel>();


            // get cart session
            //if (Session.GetString(_cartItesmSessionKey) != null)
            //  cartItems = JsonConvert.DeserializeObject<List<CartItemModel>>(Session.GetString(_cartItesmSessionKey));

            foreach (var item in cartItems)
            {
                var currentItem = _context.Category.SingleOrDefault(c => c.Id == item.Id);
                if (currentItem != null)
                {
                    var newOrderItem = new OrderItem
                    {
                        OrderId = orderEntity.Id,
                        ProductId = item.Id,
                        Quantity = item.Quantity,
                        UnitPrice = item.Price,
                        TotalPrice = (item.Price * item.Quantity)
                    };

                    orderItemEntities.Add(newOrderItem);
                    totalOrderPrice += newOrderItem.TotalPrice;
                }
            }

            // check if the order have item/s
            if (orderItemEntities.Count > 0)
            {
                orderEntity.Items = orderItemEntities;
                orderEntity.TotalCost = totalOrderPrice;
                orderEntity.TotalDiscount = 0;

                // save
                _context.Order.Add(orderEntity);

                // clear cart session
                HttpContext.Session.Remove("OrderItem");

                return RedirectToAction("OrderHistoryList", "Manage");
            }

            // something went wrong
            cartItems = new List<CartItemModel>();
            return RedirectToAction("Index", "Cart", cartItems);
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

        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            var userId = User.Identity.GetUserId();
            return _userManager.FindByIdAsync(userId);
        }

        #endregion
    }
}