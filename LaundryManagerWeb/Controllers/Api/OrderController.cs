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
using Microsoft.AspNet.Identity;

namespace WebApplication2.Controllers.Api
{
    public class OrderController : ApiController
    {
        private ApplicationDbContext _context;

        public OrderController()
        {
            _context = new ApplicationDbContext();
        }
        //GET /api/order/1
        public IHttpActionResult GetOrder(int id)
        {
            var order = _context.Order.SingleOrDefault(c => c.Id == id);

            if (order == null)
                return NotFound();

            return Ok(Mapper.Map<Order, OrderDto>(order));
        }

        //GET /api/order
        public IEnumerable<OrderDto> GetOrder()
        {
            

            if (User.IsInRole(RoleName.Customer))
            {
                var userId = User.Identity.GetUserId().ToString();
                var query = _context.Order
                .Where(m => m.Status != 99)
                .Where(m => m.UserId == userId);

                return query
                .ToList()
                .Select(Mapper.Map<Order, OrderDto>)
                .OrderBy(order => order.CreatedAt);

            } else
            {
                var query = _context.Order
                .Where(m => m.Status != 99);

                return query
                .ToList()
                .Select(Mapper.Map<Order, OrderDto>)
                .OrderBy(order => order.CreatedAt);
            }
        }

        //PUT api/order/1
        [HttpPut]
        public IHttpActionResult PutOrder(int id, OrderDto orderDto)
        {
            var selectedOrder = _context.Order.SingleOrDefault(c => c.Id == id);

            if (selectedOrder == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            selectedOrder.Status = orderDto.Status;

            _context.SaveChanges();


            _context.SaveChanges();
            return Ok(selectedOrder);


        }

    }

}
