using Book.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BooK.DataAccessLayer.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, CategoryName = "Sci-Fi", DisplayOrder = 1 },
                new Category { Id = 2, CategoryName = "Fiction", DisplayOrder = 2 },
                new Category { Id = 3, CategoryName = "Horror", DisplayOrder = 3 },
                new Category { Id = 4, CategoryName = "Fantasy", DisplayOrder = 4 },
                new Category { Id = 5, CategoryName = "Thriller", DisplayOrder = 5 },
                new Category { Id = 6, CategoryName = "Biography", DisplayOrder = 6 },
                new Category { Id = 7, CategoryName = "Poetry", DisplayOrder = 7 }
                ) ;

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Title = "Dune",
                    Author = "Frank Herbert",
                    Description = "Dune is a 1965 epic science fiction novel by American author Frank Herbert, originally published as two separate serials in Analog magazine. It tied with Roger Zelazny's This Immortal for the Hugo Award in 1966 and it won the inaugural Nebula Award for Best Novel. It is the first installment of the Dune Chronicles.",
                    ISBN = "9780441014057",
                    ListPrice = 99,
                    BulkPrice = 90,
                    BulkPriceFifty = 85,
                    BulkPriceHundred = 80,
                    CategoryId = 1,
                    ImgUrl = ""
                },
                new Product
                {
                    Id = 2,
                    Title = "Dark Skies",
                    Author = "Nancy Hoover",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN = "9781357530100",
                    ListPrice = 40,
                    BulkPrice = 30,
                    BulkPriceFifty = 25,
                    BulkPriceHundred = 20,
                    CategoryId = 1,
                    ImgUrl = ""
                },
                new Product
                {
                    Id = 3,
                    Title = "Vanish in the Sunset",
                    Author = "Julian Button",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN = "9781357530159",
                    ListPrice = 55,
                    BulkPrice = 50,
                    BulkPriceFifty = 40,
                    BulkPriceHundred = 35,
                    CategoryId = 1,
                    ImgUrl = ""
                },
                new Product
                {
                    Id = 4,
                    Title = "Cotton Candy",
                    Author = "Abby Muscles",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN = "9781357530156",
                    ListPrice = 70,
                    BulkPrice = 65,
                    BulkPriceFifty = 60,
                    BulkPriceHundred = 55,
                    CategoryId = 2,
                    ImgUrl = ""
                },
                new Product
                {
                    Id = 5,
                    Title = "Rock in the Ocean",
                    Author = "Ron Parker",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN = "9781357530152",
                    ListPrice = 30,
                    BulkPrice = 27,
                    BulkPriceFifty = 25,
                    BulkPriceHundred = 20,
                    CategoryId = 2,
                    ImgUrl = ""
                },
                new Product
                {
                    Id = 6,
                    Title = "Leaves and Wonders",
                    Author = "Laura Phantom",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN = "9781357530150",
                    ListPrice = 25,
                    BulkPrice = 23,
                    BulkPriceFifty = 22,
                    BulkPriceHundred = 20,
                    CategoryId = 2,
                    ImgUrl = ""
                }
                );
        }
    }
}
