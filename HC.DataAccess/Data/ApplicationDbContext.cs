using System;
using System.Collections.Generic;
using System.Text;
using HC.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HC.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Category> Category { get; set; }
        public DbSet<ApplicationUser> AppUser { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductImage> ProductImage { get; set; }
        public DbSet<ProductReview> ProductReview { get; set; }
        public DbSet<Unit> Unit { get; set; }

        public DbSet<Order> Order { get; set; }

        public DbSet<OrderDetail> OrderDetail { get; set; }


        public DbSet<PricingHistory> PricingHistories { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            base.OnModelCreating(modelbuilder);



            modelbuilder.Entity(typeof(Unit))
               .HasOne(typeof(Category), "Category")
               .WithMany()
               .HasForeignKey("CategoryId")
               .OnDelete(DeleteBehavior.Cascade);  // no ON DELETE


                modelbuilder.Entity(typeof(Product))
              .HasOne(typeof(Category), "Category")
              .WithMany()
              .HasForeignKey("CategoryId")
              .OnDelete(DeleteBehavior.NoAction);  // no ON DELETE



            modelbuilder.Entity(typeof(Product))
                .HasOne(typeof(Unit), "Unit")
                .WithMany()
                .HasForeignKey("UnitId")
                .OnDelete(DeleteBehavior.NoAction);  // no ON DELETE



            modelbuilder.Entity(typeof(Product))
             .HasOne(typeof(ApplicationUser), "User")
             .WithMany()
             .HasForeignKey("UserId")
             .OnDelete(DeleteBehavior.NoAction);  // no ON DELETE


            modelbuilder.Entity(typeof(ProductReview))
            .HasOne(typeof(ApplicationUser), "User")
            .WithMany()
            .HasForeignKey("UserId")
            .OnDelete(DeleteBehavior.NoAction);  // no ON DELETE

            
        }
    }
}
