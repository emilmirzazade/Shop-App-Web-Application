using Microsoft.EntityFrameworkCore;
using shopapp.entityy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace shopapp.data.Concretee.EfCore
{
    public static class SeedDatabase
    {
        public static void Seed()
        {
            var context = new ShopContext();
            if (context.Database.GetPendingMigrations().Count() == 0)
            {
                if (context.Categories.Count() == 0)
                {
                    context.Categories.AddRange(Categories);
                }
                if (context.Products.Count() == 0)
                {
                    context.Products.AddRange(Products);
                    context.AddRange(ProductCategories);
                }
            }
            context.SaveChanges();
        }
        private static Category[] Categories = {
            new Category(){Name="Car", Url="car"},
            new Category(){Name="Computer", Url="computer"},
            new Category(){Name="Electronics",Url="electronics"},
            new Category(){Name="White things",Url="white-things"}
        };
        private static Product[] Products = {
            new Product(){Name="BMV", Url="bmv-1", Price=20000, ImageUrl="1.jpg", Description="Good", IsApproved=true},
            new Product(){Name="Mercedes", Url="mercedes", Price=30000, ImageUrl="2.jpg", Description="Good", IsApproved=false},
            new Product(){Name="Kia", Url="kia", Price=40000, ImageUrl="3.jpg", Description="Good", IsApproved=true},
            new Product(){Name="BMV", Url="bmv-2", Price=50000, ImageUrl="4.jpg", Description="Good", IsApproved=false},
            new Product(){Name="Acura", Url="acura", Price=60000, ImageUrl="1.jpg", Description="Good", IsApproved=true}
        };

        private static ProductCategory[] ProductCategories ={
            new ProductCategory(){Product=Products[0], Category=Categories[0]},
            new ProductCategory(){Product=Products[0], Category=Categories[2]},
            new ProductCategory(){Product=Products[1], Category=Categories[0]},
            new ProductCategory(){Product=Products[1], Category=Categories[2]},
            new ProductCategory(){Product=Products[2], Category=Categories[0]},
            new ProductCategory(){Product=Products[2], Category=Categories[2]},
            new ProductCategory(){Product=Products[3], Category=Categories[0]},
            new ProductCategory(){Product=Products[3], Category=Categories[2]}
        };
    }
}
