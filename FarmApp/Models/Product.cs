using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FarmApp.Models
{
    public abstract class BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long Id { get; set; }
    }


    public class Product : BaseEntity
    {
        public string Title { get; set; }

        public ProductType Type { get; set; }

        public string Description { get; set; }

        public string OwnerId { get; set; }
    }

    public enum ProductType
    {
        Milk,
        Eggs
    }

    public class ProductDbContext : DbContext
    {
        public ProductDbContext()
            : base("DefaultConnection")
        { }

        public DbSet<Product> Products { get; set; }

        public static ProductDbContext Create()
        {
            return new ProductDbContext();
        }
    }
}