using Microsoft.EntityFrameworkCore;

namespace FirstMVCApp.Models
{
    public class ProductsDbContextcs : DbContext
    {
        public DbSet<Product> Products { get; set;}
        
        public ProductsDbContextcs(DbContextOptions<ProductsDbContextcs> options) : base(options) { }
        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                connectionString: @"server=(local);database=northwind;integrated security=sspi;trustservercertificate=true"
                );
        }*/
    }
}
