﻿using Discount.Grpc.Model;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data
{
    public class DiscountContext : DbContext
    {
        public DiscountContext(DbContextOptions<DiscountContext> options) : base(options) 
        {
            
        }

        public DbSet<Coupon> Coupons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coupon>().HasData(
                new Coupon
                {
                    Id = 1,
                    ProductName = "Iphone",
                    Description = "Iphone 15 pro max",
                    Amount = 300000
                },
                new Coupon
                {
                    Id = 2,
                    ProductName = "Samsung",
                    Description = "Samsaung galaxy S20",
                    Amount = 200000
                }
                );
        }
    }
}
