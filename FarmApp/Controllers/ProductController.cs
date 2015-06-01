using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FarmApp.Models;
using FarmApp.ViewModels;
using Microsoft.AspNet.Identity;

namespace FarmApp.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            using (ProductDbContext ctx = ProductDbContext.Create())
            {
                string userId = User.Identity.GetUserId();
                var products = ctx.Products.Where(p => p.OwnerId == userId).Select(p=>new ProductViewModel()
                {
                    Description = p.Description,
                    Title = p.Title,
                    Type = p.Type
                }).ToList();

                return View(products);
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ProductViewModel viewModel)
        {
            using (ProductDbContext ctx = ProductDbContext.Create())
            {
                ctx.Products.Add(new Product()
                {
                    Description = viewModel.Description,
                    Title = viewModel.Title,
                    Type = viewModel.Type,
                    OwnerId = User.Identity.GetUserId()
                });

                ctx.SaveChanges();
            }

            return RedirectToAction("Create");
        }
    }
}