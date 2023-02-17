using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowCarrot.Models;

namespace YellowCarrot.Data;

public class AppDbContext: DbContext
{
    public AppDbContext()
    {
    }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<Recipe> Recipes { get; set; }

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=YellowCarrot;Trusted_Connection=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		//Start receptet
		modelBuilder.Entity<Recipe>().HasData(new Recipe()
		{
			RecipeId = 1,
			Name = "Makaroner och Köttbullar",
			TagId = 1
		});
		//Ingredienserna till ovannämnda receptet
		modelBuilder.Entity<Ingredient>().HasData(new Ingredient()
		{
			IngredientId = 1,
			Name = "Macaroner",
			Quantity = "4 dl",
			RecipeId = 1
		}, new Ingredient()
		{
            IngredientId = 2,
            Name = "Köttbullar",
            Quantity = "Ett dussin",
            RecipeId = 1
        });

		modelBuilder.Entity<Tag>().HasData(new Tag()
		{
			TagId = 1,
			TagName = "Husmanskost"
		},
		new Tag()
		{
			TagId = 2,
			TagName = "Medelshavsmat"
		},
		new Tag()
        {
            TagId = 3,
            TagName = "Vegetarisk"
        },
        new Tag()
        {
            TagId = 4,
            TagName = "Vegansk"
        },
        new Tag()
        {
            TagId = 5,
            TagName = "Asiatisk"
        });
	}

}
