using EcommerceAPI.Models.AuthModels;
using EcommerceAPI.Models.CouponModels;
using EcommerceAPI.Models.ProductModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using IdentityDbContext = Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General.IdentityDbContext;

namespace EcommerceAPI.Data;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
     public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
     {
          
     }

     public DbSet<Coupon> Coupons { get; set; }

     public DbSet<Product> Products { get; set; }
     public DbSet<ApplicationUser> ApplicationUsers { get; set; }

     protected override void OnModelCreating(ModelBuilder modelBuilder)
     {
          base.OnModelCreating(modelBuilder);

          modelBuilder.Entity<Coupon>().HasData(new Coupon
          {
              CouponId = 1,
              CouponCode = "10OFF",
              DiscountAmount = 10,
              MinAmount = 20
          });
          modelBuilder.Entity<Coupon>().HasData(new Coupon
          {
              CouponId = 2,
              CouponCode = "20OFF",
              DiscountAmount = 20,
              MinAmount = 40
          });

          modelBuilder.Entity<Product>().HasData(new Product
          {
              ProductId = 1,
              Name = "Samosa",
              Price = 15,
              Description = " Quisque vel lacus ac magna, vehicula sagittis ut non lacus.<br/> Vestibulum arcu turpis, maximus malesuada neque. Phasellus commodo cursus pretium.",
              ImageURL = "https://placehold.co/603x403",
              CategoryName = "Appetizer"
          });
          modelBuilder.Entity<Product>().HasData(new Product
          {
              ProductId = 2,
              Name = "Paneer Tikka",
              Price = 13.99,
              Description = " Quisque vel lacus ac magna, vehicula sagittis ut non lacus.<br/> Vestibulum arcu turpis, maximus malesuada neque. Phasellus commodo cursus pretium.",
              ImageURL = "https://placehold.co/602x402",
              CategoryName = "Appetizer"
          });
          modelBuilder.Entity<Product>().HasData(new Product
          {
              ProductId = 3,
              Name = "Sweet Pie",
              Price = 10.99,
              Description = " Quisque vel lacus ac magna, vehicula sagittis ut non lacus.<br/> Vestibulum arcu turpis, maximus malesuada neque. Phasellus commodo cursus pretium.",
              ImageURL = "https://placehold.co/601x401",
              CategoryName = "Dessert"
          });
          modelBuilder.Entity<Product>().HasData(new Product
          {
              ProductId = 4,
              Name = "Pav Bhaji",
              Price = 15,
              Description = " Quisque vel lacus ac magna, vehicula sagittis ut non lacus.<br/> Vestibulum arcu turpis, maximus malesuada neque. Phasellus commodo cursus pretium.",
              ImageURL = "https://placehold.co/600x400",
              CategoryName = "Entree"
          });
     }

    


          

}