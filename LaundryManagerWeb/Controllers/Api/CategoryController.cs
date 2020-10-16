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
    public class CategoryController : ApiController
    {
        private ApplicationDbContext _context;

        public CategoryController()
        {
            _context = new ApplicationDbContext();
        }
        //GET /api/category/1
        public IHttpActionResult GetCategory(int id)
        {
            var category = _context.Category.SingleOrDefault(c => c.Id == id);

            if (category == null)
                return NotFound();

            return Ok(Mapper.Map<Category, CategoryDto>(category));
        }

        //GET /api/category
        public IEnumerable<CategoryDto> GetCategory()
        {
            var query = _context.Category
                .Where(m => m.Status != 4);
            return query
                .ToList()
                .Select(Mapper.Map<Category, CategoryDto>);
        }

        //DELETE api/category/1
        [HttpDelete]
        public IHttpActionResult DeleteCategory(int id)
        {
            var selectedCategory = _context.Category.SingleOrDefault(c => c.Id == id);

            if (selectedCategory == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Category.Remove(selectedCategory);
            _context.SaveChanges();
            return Ok(selectedCategory);


        }

    }

}
