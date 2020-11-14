using AutoMapper;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using LaundryManagerWeb.Dtos;
using LaundryManagerWeb.Models;

namespace WebApplication2.Controllers.Api
{
    public class CustomerController : ApiController
    {
        private ApplicationDbContext _context;

        public CustomerController()
        {
            _context = new ApplicationDbContext();
        }

        //GET /api/customer
        public IEnumerable<ApplicationUser> GetCustomer()
        {
            return _context.Users
                .Include(u => u.Roles)
                .ToList();
        }

    }
}
