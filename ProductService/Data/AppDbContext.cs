using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using ProductService.Models;

namespace ProductService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {

        }

        public DbSet<Product> Products { get; set; }
        
        

    }
}