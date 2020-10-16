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
    public class CategoryController : Controller
    {
        private ApplicationDbContext _context;
        private string userId;

        public CategoryController()
        {
            _context = new ApplicationDbContext();
            userId = ""; // User.Identity.GetUserId();

        }

        // GET: Category/Random 

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Category
        public ActionResult Index()
        {
            var list = GlobalFunctions.MesureTypes();
            ViewData["list"] = list;

            if (User.IsInRole(RoleName.Admin))
                return View();
            else
                return View("../Shared/NoPermission");
        }


        // GET: Create ApartmentType
        [Authorize(Roles = RoleName.Admin)]
        public ActionResult Create()
        {
            var list = GlobalFunctions.MesureTypes();

            var viewModel = new CategoryFormViewModel
            {
                Category = new Category()
            };
            ViewData["list"] = list;
            return View("CategoryForm", viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.Admin)]
        public ActionResult Save(Category category)
        {
            //Model.State to check validation from the model
            if (!ModelState.IsValid)
            {
                var viewModel = new CategoryFormViewModel
                {
                    Category = category
                };

                return View("CategoryForm", viewModel);
            }
            try
            {
                if (category.Id == 0)
                {
                    category.CreatedAt = DateTime.Now;
                    category.ModifiedAt = DateTime.Now;
                    category.CreatedBy = userId;
                    category.ModifiedBy = userId;
                    _context.Category.Add(category);

                }
                else
                {
                    var selectedCategory = _context.Category.Single(m => m.Id == category.Id);
                    selectedCategory.Name = category.Name;
                    selectedCategory.UnitType = category.UnitType;
                    selectedCategory.UnitCharge = category.UnitCharge;
                    selectedCategory.ModifiedAt = DateTime.Now;
                    selectedCategory.ModifiedBy = userId;

                }

                _context.SaveChanges();

                return RedirectToAction("index", "Category");

            }
            catch (Exception e)
            {
                return RedirectToAction("index", "Category");
            }

        }

        [Authorize(Roles = RoleName.Admin)]
        public ActionResult Edit(int id)
        {
            var category = _context.Category.SingleOrDefault(c => c.Id == id);
            if (category == null)
                return HttpNotFound();

            var list = GlobalFunctions.MesureTypes();

            var viewModel = new CategoryFormViewModel
            {
                Category = category

            };
            ViewData["list"] = list;
            return View("CategoryForm", viewModel);
        }
    }
}