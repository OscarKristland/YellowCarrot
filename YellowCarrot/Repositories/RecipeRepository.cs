using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YellowCarrot.Data;
using YellowCarrot.Interface;
using YellowCarrot.Models;

namespace YellowCarrot.Repositories;

public class RecipeRepository : Repository<Recipe>, IRecipeRepository
{
    public readonly AppDbContext _context;

    public RecipeRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public List<Recipe> GetAllRecipesWithIngredients()
    {
        return _context.Recipes.Include(r => r.Ingredients).ToList();
    }

    public Recipe GetRecipeWithIngredients(int id)
    {
        return _context.Recipes.Include(r => r.Ingredients).First(r => r.RecipeId == id);
    }

}
