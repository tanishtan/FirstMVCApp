using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace NorthwindAPI.Models
{
    public class Product
    {
        public int ProductId { get; set; }


        [Required(ErrorMessage = "Product Name is Required")]
        [MinLength(4, ErrorMessage = "Product Name should be 4 or more characters")]
        [StringLength(50, ErrorMessage = "Product Name cannot exceed 50 characters")]
        public string ProductName { get; set; } = "";


        [Required(ErrorMessage = "Unit Price is Required")]
        [Range(minimum: 1, maximum: 10000, ErrorMessage = "Unit price should be between 1 and 1000")]
        [Display(Name = "Price per Unit")]
        public decimal UnitPrice { get; set; }


        [Required(ErrorMessage = "Units in stock is required")]
        [Range(minimum: 1, maximum: 1000, ErrorMessage = "Units in stock must be between 1 and 1000")]
        [Display(Name = "Stock Level")]
        public short UnitsInStock { get; set; }

        //StringLength, Compare, CreditCard, Datatype, Display, EmailAddress, Key, Phone, RegularExpression, Url

        public bool Discontinued { get; set; } = false;
    }

    public class ProductsDbContext : DbContext
    {
        public ProductsDbContext(DbContextOptions<ProductsDbContext> options) : base(options) { }
        public DbSet<Product> Products { get; set;}
    }
}
