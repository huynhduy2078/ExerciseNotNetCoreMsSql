using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExerciseOne.Models
{
    public static class PrepDB
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<ProductContext>());
            }
        }

        private static void SeedData(ProductContext context)
        {
            System.Console.WriteLine("Appling Migration...");

            context.Database.Migrate();
            if (!context.Products.Any())
            {
                System.Console.WriteLine("Adding data -seeding...");
                context.SaveChanges();
            }
            else
            {
                System.Console.WriteLine("Already have dâta - not seeding...");
            }
        }
    }
}
