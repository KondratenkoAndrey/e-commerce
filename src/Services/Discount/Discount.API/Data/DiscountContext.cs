using Discount.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Discount.API.Data;

public class DiscountContext : DbContext
{
    public DbSet<Coupon> Coupons { get; set; }

    public DiscountContext(DbContextOptions<DiscountContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Coupon>().Property(b => b.Id).IsRequired();
        modelBuilder.Entity<Coupon>().Property(b => b.ProductName).IsRequired();
    }
}