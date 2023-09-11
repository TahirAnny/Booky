﻿// <auto-generated />
using BooK.DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BooK.DataAccessLayer.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230911022718_imageUrlAddedToProductTable")]
    partial class imageUrlAddedToProductTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0-preview.7.23375.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Book.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("DisplayOrder")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryName = "Sci-Fi",
                            DisplayOrder = 1
                        },
                        new
                        {
                            Id = 2,
                            CategoryName = "Fiction",
                            DisplayOrder = 2
                        },
                        new
                        {
                            Id = 3,
                            CategoryName = "Horror",
                            DisplayOrder = 3
                        },
                        new
                        {
                            Id = 4,
                            CategoryName = "Fantasy",
                            DisplayOrder = 4
                        },
                        new
                        {
                            Id = 5,
                            CategoryName = "Thriller",
                            DisplayOrder = 5
                        },
                        new
                        {
                            Id = 6,
                            CategoryName = "Biography",
                            DisplayOrder = 6
                        },
                        new
                        {
                            Id = 7,
                            CategoryName = "Poetry",
                            DisplayOrder = 7
                        });
                });

            modelBuilder.Entity("Book.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("BulkPrice")
                        .HasColumnType("float");

                    b.Property<double>("BulkPriceFifty")
                        .HasColumnType("float");

                    b.Property<double>("BulkPriceHundred")
                        .HasColumnType("float");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ISBN")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImgUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("ListPrice")
                        .HasColumnType("float");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Author = "Frank Herbert",
                            BulkPrice = 90.0,
                            BulkPriceFifty = 85.0,
                            BulkPriceHundred = 80.0,
                            CategoryId = 1,
                            Description = "Dune is a 1965 epic science fiction novel by American author Frank Herbert, originally published as two separate serials in Analog magazine. It tied with Roger Zelazny's This Immortal for the Hugo Award in 1966 and it won the inaugural Nebula Award for Best Novel. It is the first installment of the Dune Chronicles.",
                            ISBN = "9780441014057",
                            ImgUrl = "",
                            ListPrice = 99.0,
                            Title = "Dune"
                        },
                        new
                        {
                            Id = 2,
                            Author = "Nancy Hoover",
                            BulkPrice = 30.0,
                            BulkPriceFifty = 25.0,
                            BulkPriceHundred = 20.0,
                            CategoryId = 1,
                            Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                            ISBN = "9781357530100",
                            ImgUrl = "",
                            ListPrice = 40.0,
                            Title = "Dark Skies"
                        },
                        new
                        {
                            Id = 3,
                            Author = "Julian Button",
                            BulkPrice = 50.0,
                            BulkPriceFifty = 40.0,
                            BulkPriceHundred = 35.0,
                            CategoryId = 1,
                            Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                            ISBN = "9781357530159",
                            ImgUrl = "",
                            ListPrice = 55.0,
                            Title = "Vanish in the Sunset"
                        },
                        new
                        {
                            Id = 4,
                            Author = "Abby Muscles",
                            BulkPrice = 65.0,
                            BulkPriceFifty = 60.0,
                            BulkPriceHundred = 55.0,
                            CategoryId = 2,
                            Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                            ISBN = "9781357530156",
                            ImgUrl = "",
                            ListPrice = 70.0,
                            Title = "Cotton Candy"
                        },
                        new
                        {
                            Id = 5,
                            Author = "Ron Parker",
                            BulkPrice = 27.0,
                            BulkPriceFifty = 25.0,
                            BulkPriceHundred = 20.0,
                            CategoryId = 2,
                            Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                            ISBN = "9781357530152",
                            ImgUrl = "",
                            ListPrice = 30.0,
                            Title = "Rock in the Ocean"
                        },
                        new
                        {
                            Id = 6,
                            Author = "Laura Phantom",
                            BulkPrice = 23.0,
                            BulkPriceFifty = 22.0,
                            BulkPriceHundred = 20.0,
                            CategoryId = 2,
                            Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                            ISBN = "9781357530150",
                            ImgUrl = "",
                            ListPrice = 25.0,
                            Title = "Leaves and Wonders"
                        });
                });

            modelBuilder.Entity("Book.Models.Product", b =>
                {
                    b.HasOne("Book.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });
#pragma warning restore 612, 618
        }
    }
}
