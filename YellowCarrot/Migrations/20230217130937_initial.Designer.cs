﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using YellowCarrot.Data;

#nullable disable

namespace YellowCarrot.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230217130937_initial")]
    partial class initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RecipeTag", b =>
                {
                    b.Property<int>("RecipesRecipeId")
                        .HasColumnType("int");

                    b.Property<int>("TagsTagId")
                        .HasColumnType("int");

                    b.HasKey("RecipesRecipeId", "TagsTagId");

                    b.HasIndex("TagsTagId");

                    b.ToTable("RecipeTag");
                });

            modelBuilder.Entity("YellowCarrot.Models.Ingredient", b =>
                {
                    b.Property<int>("IngredientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IngredientId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Quantity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.HasKey("IngredientId");

                    b.HasIndex("RecipeId");

                    b.ToTable("Ingredients");

                    b.HasData(
                        new
                        {
                            IngredientId = 1,
                            Name = "Macaroner",
                            Quantity = "4 dl",
                            RecipeId = 1
                        },
                        new
                        {
                            IngredientId = 2,
                            Name = "Köttbullar",
                            Quantity = "Ett dussin",
                            RecipeId = 1
                        });
                });

            modelBuilder.Entity("YellowCarrot.Models.Recipe", b =>
                {
                    b.Property<int>("RecipeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RecipeId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TagId")
                        .HasColumnType("int");

                    b.HasKey("RecipeId");

                    b.ToTable("Recipes");

                    b.HasData(
                        new
                        {
                            RecipeId = 1,
                            Name = "Makaroner och Köttbullar",
                            TagId = 1
                        });
                });

            modelBuilder.Entity("YellowCarrot.Models.Tag", b =>
                {
                    b.Property<int>("TagId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TagId"));

                    b.Property<string>("TagName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TagId");

                    b.ToTable("Tags");

                    b.HasData(
                        new
                        {
                            TagId = 1,
                            TagName = "Husmanskost"
                        },
                        new
                        {
                            TagId = 2,
                            TagName = "Medelshavsmat"
                        },
                        new
                        {
                            TagId = 3,
                            TagName = "Vegetarisk"
                        },
                        new
                        {
                            TagId = 4,
                            TagName = "Vegansk"
                        },
                        new
                        {
                            TagId = 5,
                            TagName = "Asiatisk"
                        });
                });

            modelBuilder.Entity("RecipeTag", b =>
                {
                    b.HasOne("YellowCarrot.Models.Recipe", null)
                        .WithMany()
                        .HasForeignKey("RecipesRecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("YellowCarrot.Models.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsTagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("YellowCarrot.Models.Ingredient", b =>
                {
                    b.HasOne("YellowCarrot.Models.Recipe", "Recipe")
                        .WithMany("Ingredients")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("YellowCarrot.Models.Recipe", b =>
                {
                    b.Navigation("Ingredients");
                });
#pragma warning restore 612, 618
        }
    }
}