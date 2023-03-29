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
			Id = 1,
			Name = "Makaroner och Köttbullar",
			TagId = 1
		});
		//Ingredienserna till ovannämnda receptet
		modelBuilder.Entity<Ingredient>().HasData(new Ingredient()
        {
            Id = 1,
            Name = "Macaroner",
            Quantity = 4,
            Unit = "dl",
            RecipeId = 1
        }, new Ingredient()
        {
            Id = 2,
            Name = "Köttbullar",
            Quantity = 1,
            Unit = "dussin",
            RecipeId = 1
        });

        modelBuilder.Entity<Tag>().HasData(new Tag()
		{
			Id = 1,
			Name = "Husmanskost"
		},
		new Tag()
		{
            Id = 2,
			Name = "Medelshavsmat"
		},
		new Tag()
        {
            Id = 3,
            Name = "Vegetarisk"
        },
        new Tag()
        {
            Id = 4,
            Name = "Vegansk"
        },
        new Tag()
        {
            Id = 5,
            Name = "Asiatisk"
        });
	}

}
