using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Models;

namespace UserService.Data
{
    public static class PrepDb
    {
        public static void PrepData(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
        }

        private static void SeedData(AppDbContext context)
        {
            if(!context.Users.Any())
            {
                Console.WriteLine("--> Seeding Data...");
                context.Users.AddRange(
                    new User() { Username = "Roy", Email = "roy@gmail.com", Password = "roy1234" },
                    new User() { Username = "Xin", Email = "xin@gmail.com", Password = "xin1234" }
                );
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("--> We already have data");
            }
        }
    }
}
