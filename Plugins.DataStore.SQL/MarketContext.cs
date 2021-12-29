using Microsoft.EntityFrameworkCore;
using CoreBusiness;
using System;

namespace Plugins.DataStore.SQL
{
    public class MarketContext : DbContext
    {
        public MarketContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId);

            // seed some data

            modelBuilder.Entity<Category>()
                .HasData(
                    new Category { CategoryId = 1, Name = "Milk", Description = "Milk is a nutrient-rich liquid food produced by the mammary glands of mammals. It is the primary source of nutrition for young mammals, including breastfed" },
                    new Category { CategoryId = 2, Name = "Meal", Description = "Meat is animal flesh that is eaten as food.[1] Humans have hunted and killed animals for meat since prehistoric times. The advent of civilization allowed the domestication of animals such as chickens, sheep, rabbits, pigs and cattle" },
                    new Category { CategoryId = 3, Name = "Vegetable", Description = "Vegetables are parts of plants that are consumed by humans or other animals as food. The original meaning is still commonly used and is applied to plants collectively to refer to all edible plant matter, including the flowers, fruits, stems, leaves, roots, and seeds" }
                );

            modelBuilder.Entity<Product>()
                .HasData(
                    new Product() { ProductId = 1, CategoryId = 1, Name = "Vinamilk", Price = 1.99, Quantity = 120 },
                    new Product() { ProductId = 2, CategoryId = 1, Name = "Dutch Lady Milk", Price = 1.70, Quantity = 80 },
                    new Product() { ProductId = 3, CategoryId = 2, Name = "Chicken Parmigiana", Price = 12.80, Quantity = 150 },
                    new Product() { ProductId = 4, CategoryId = 3, Name = "Chokos", Price = 5.66, Quantity = 50 }
                );
        }
    }
}
