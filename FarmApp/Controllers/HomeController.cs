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
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (ProductDbContext ctx = ProductDbContext.Create())
            {
                var products = ctx.Products.Select(p => new ProductViewModel()
                {
                    Description = p.Description,
                    Title = p.Title,
                    Type = p.Type,
                    Id = p.Id
                }).ToList();

                return View(products);
            }
        }

        [HttpPost]
        public ActionResult Search(string query)
        {
            query = query.Trim();
            
            if (string.IsNullOrEmpty(query))
            {
                return RedirectToAction("Index");
            }

            using (ProductDbContext ctx = ProductDbContext.Create())
            {
                var products = ctx.Products.Where(p => p.Title.Contains(query)).Select(p => new ProductViewModel()
                {
                    Description = p.Description,
                    Title = p.Title,
                    Type = p.Type,
                    Id = p.Id
                }).ToList();

                ViewBag.Query = query;

                return View("Index", products);
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}