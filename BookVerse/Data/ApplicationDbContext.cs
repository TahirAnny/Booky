using BookVerse.Models;
using Microsoft.EntityFrameworkCore;

namespace BookVerse.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, CategoryName = "Sci-Fi", DisplayOrder = 1 },
                new Category { Id = 2, CategoryName = "Fiction", DisplayOrder = 2 },
                new Category { Id = 3, CategoryName = "Horror", DisplayOrder = 3 },
                new Category { Id = 4, CategoryName = "Fantasy", DisplayOrder = 4 },
                new Category { Id = 5, CategoryName = "Thriller", DisplayOrder = 5 },
                new Category { Id = 6, CategoryName = "Biography", DisplayOrder = 6 },
                new Category { Id = 7, CategoryName = "Poetry", DisplayOrder = 7 }
                ) ;
        }
    }
}
