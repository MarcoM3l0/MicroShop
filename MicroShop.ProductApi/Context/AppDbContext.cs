using MicroShop.ProductApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroShop.ProductApi.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

    // Fluent API
    protected override void OnModelCreating(ModelBuilder mb)
    {
        // Category
        mb.Entity<Category>()
     .HasKey(c => c.CategoryId);

        mb.Entity<Category>()
            .Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);

        // Product
        mb.Entity<Product>()
            .HasKey(p => p.ProductId);

        mb.Entity<Product>()
            .Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);

        mb.Entity<Product>()
            .Property(p => p.Description)
            .IsRequired()
            .HasMaxLength(255);

        mb.Entity<Product>()
            .Property(p => p.ImageURL)
            .IsRequired()
            .HasMaxLength(255);

        mb.Entity<Product>()
            .Property(p => p.Price)
            .HasPrecision(12, 2);

        mb.Entity<Category>()
            .HasMany(c => c.Products)
            .WithOne(p => p.Category)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        // Seed Data
        mb.Entity<Category>().HasData(
            new Category { CategoryId = 1, Name = "Material Escolar" },
            new Category { CategoryId = 2, Name = "Eletrônicos" }
        );

    }
}
