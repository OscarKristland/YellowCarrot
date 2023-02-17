using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowCarrot.Data;
using YellowCarrot.Interface;
using YellowCarrot.Models;

namespace YellowCarrot.Repositories;

public class IngredientRepository: IIngredientRepository
{
    private readonly AppDbContext _context;

    public IngredientRepository(AppDbContext context)
    {
        _context = context;
    }

    //Få alla ingridienser
    public List<Ingredient> GetIngredients()
    {
        return _context.Ingredients.ToList();
    }

    //Lägg till en ingridient
    public void AddIngredient(Ingredient ingredientToAdd)
    {
        _context.Ingredients.Add(ingredientToAdd);
    }

    //Ta bort en ingridient
    public void IngredientToRemove(Ingredient ingredientToRemove)
    {
        _context.Ingredients.Remove(ingredientToRemove);
    }
}
