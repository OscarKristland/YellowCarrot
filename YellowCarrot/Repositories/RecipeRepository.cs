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

public class RecipeRepository: IRecipeRepository
{
    private readonly AppDbContext _context;

    public RecipeRepository(AppDbContext context)
    {
        _context = context;
    }

    //Få recept, med ingredienser plus tagg
    public List<Recipe> GetRecipes()
    {
        return _context.Recipes.Include(r => r.Ingredients).Include(r => r.Tags).ToList();
    }

    //Få id, med ingredienser plus tagg
    public Recipe? GetRecipe(int id)
    {
        return _context.Recipes.Include(r => r.Ingredients).Include(r=>r.Tags).FirstOrDefault(r => r.RecipeId == id);
    }

    public void AddRecipe(Recipe recipeToAdd)
    {
        _context.Recipes.Add(recipeToAdd);
    }

    public void UpdateRecipe(Recipe recipeToUpdate)
    {
        _context.Recipes.Update(recipeToUpdate);
    }

    public void DeleteRecipe(Recipe recipeToRemove)
    {
        _context.Recipes.Remove(recipeToRemove);
    }

    public void AddIngredientToRcipe(Ingredient IngredientToAdd)
    {
        _context.Recipes.Add(GetRecipe(IngredientToAdd.RecipeId));
    }

}
