using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
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
                    Type = p.Type,
                    Id = p.Id
                }).ToList();

                return View(products);
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(ProductViewModel viewModel)
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

                await ctx.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Delete(long id)
        {
            using (ProductDbContext ctx = ProductDbContext.Create())
            {
                ctx.Products.Remove(ctx.Products.Single(p => p.Id == id));

                await ctx.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Edit(long id)
        {
            using (ProductDbContext ctx = ProductDbContext.Create())
            {
                string userId = User.Identity.GetUserId();

                var product = await ctx.Products.SingleOrDefaultAsync(p=>p.Id == id && p.OwnerId == userId);

                if (product != null)
                {
                    return
                        View(new ProductViewModel()
                        {
                            Description = product.Description,
                            Title = product.Title,
                            Type = product.Type,
                            Id = product.Id
                        });
                }

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Edit(ProductViewModel viewModel)
        {
            string userId = User.Identity.GetUserId();
            Product product;

            using (ProductDbContext ctx = ProductDbContext.Create())
            {
                product =
                    ctx.Products.SingleOrDefault(p => p.Id == viewModel.Id && p.OwnerId == userId);
            }

            using (ProductDbContext ctx = ProductDbContext.Create())
            {
                if (product != null)
                {
                    ctx.Entry(new Product()
                    {
                        Id = viewModel.Id,
                        Description = viewModel.Description,
                        Title = viewModel.Title,
                        Type = viewModel.Type,
                        OwnerId = userId
                    }).State = EntityState.Modified;

                    await ctx.SaveChangesAsync();
                }
            }

            return RedirectToAction("Index");
        }

        public ActionResult Detail(long id)
        {
            using (ProductDbContext ctx = ProductDbContext.Create())
            {
                string userId = User.Identity.GetUserId();
                var product = ctx.Products.SingleOrDefault(p => p.Id == id && p.OwnerId == userId);

                if (product != null)
                {
                    return View(new ProductViewModel()
                    {
                        Description = product.Description,
                        Title = product.Title,
                        Type = product.Type,
                        Id = product.Id
                    });
                }

                return RedirectToAction("Index");
            }
        } 
    }
}