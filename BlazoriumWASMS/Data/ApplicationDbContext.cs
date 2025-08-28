using BlazoriumWASMS.Shared.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlazoriumWASMS.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Επειδή έχω Identity στον πίνακα δεν σβήνω τον κώδικα της γραμμής αυτής γιατί χρειάζεται για το seeding.
            base.OnModelCreating(modelBuilder); 

            modelBuilder.Entity<Category>().ToTable("Categories");

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Appetizer" },
                new Category { Id = 2, Name = "Entree" },
                new Category { Id = 3, Name = "Dessert" }
            );
        }
    }
}
