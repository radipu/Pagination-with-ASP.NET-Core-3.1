using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pagination.Data;
using Pagination.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pagination.Controllers
{
    public class ProductController : Controller
    {
        private readonly PaginationContext _context;
        public ProductController(PaginationContext context)
        {
            _context = context;
        }
        /*public IActionResult Index()
        {
            List<Product> products = _context.Products.ToList();
            return View(products);
        }*/

        [HttpGet]
        

        //Pagination
        public IActionResult Index(int pg = 1)
        {
            List<Product> products = _context.Products.ToList();
            const int pageSize = 5;
            if (pg < 1)
                pg = 1;
            int recsCount = products.Count();
            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = products.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;
            return View(data);
        }
        //~Pagination
        [HttpGet]
        public IActionResult Create()
        {
            Product prod = new Product();
            return View(prod);
        }
        [HttpPost]
        public IActionResult Create(Product product)
        {
            _context.Add(product);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}