using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using AbrasNigEnt.Data.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace AbrasNigEnt.Data
{
    public class DbInitializer
    {
      
        public static void SeedDatabase(IServiceProvider serviceProvider)
        {
            AppDbContext context = serviceProvider.GetRequiredService<AppDbContext>();
            context.Database.EnsureDeleted();
            context.Database.Migrate();

            List<Category> categories = new List<Category>
            {
                new Category{ Name = "Filters", Description = "Removes dirt from oil and air"},
                new Category{ Name = "O-Ring", Description = "Seals oil in"}
            };

            List<Brand> brands = new List<Brand>
            {
                new Brand{Name = "CAT"},
                new Brand{Name = "Komatsu"}
            };

            List<Product> products = new List<Product>
            {
                new Product{PartNumber = "600-105-455", Category = categories.ElementAt(0), Brand = brands.ElementAt(0) },
                new Product{PartNumber = "4550-135-199", Category= categories.ElementAt(1), Brand = brands.ElementAt(1) }
            };


            if (!context.Brands.Any() || !context.Categories.Any() || true)
            {
                context.Brands.AddRange(brands);
                context.Categories.AddRange(categories);
                context.Products.AddRange(products);
            }
            context.SaveChanges();


        }
    }
}
