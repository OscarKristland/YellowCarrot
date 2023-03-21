using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using YellowCarrot.Data;
using YellowCarrot.Models;
using YellowCarrot.Repositories;

namespace YellowCarrot.Manager;

public static class WindowManager
{
    private static readonly AppDbContext _context;

    //hantera alla fönster, alltså alla klick//
    public static void AddLvItemToLv(ListViewItem lvItem, ListView listView)
    {
        listView.Items.Add(lvItem);
    }

    //Ta bort ett recept
    public static void RemoveRecipe(Recipe recipeToRemove)
    {
        _context.Recipes.Remove(recipeToRemove);
    }

    //Skapa ett recept
    public static Recipe CreateRecipe(string name,List<Ingredient>ingredients)
    {
        //namn
        //minst 2 ingredienser
        
        Recipe recipe = new();
        recipe.Name = name;
        recipe.Ingredients = ingredients;

        return recipe;
    }

    //Lägga till ingridiens
    public static Ingredient CreateIngredient(string name, int quantity, string unit)
    {
        Ingredient ingredient = new();
        ingredient.Name = name;
        ingredient.Quantity = quantity;
        ingredient.Units = unit;
        return ingredient;
    }

    //Går att skapa recept om det finns fler än 3
    public static bool Checking(string name, List<Ingredient>ingredients)
    {
        if (ingredients.Count() > 1)
        {
            if (!String.IsNullOrEmpty(name))
            {
                return true;
            }
        }
        return false;
    }



    //laddar en cbobox från en generell lista
    public static void LoadCboBox<T>(List<T>list, ComboBox cbo)
    {
        cbo.Items.Clear();
        foreach (var item in list)
        {
            cbo.Items.Add(item.ToString());
        }
    }

    public static void LoadAllUnits(ComboBox comboBox)
    {
        //cbo läggas till i
        //for
        comboBox.ItemsSource = Enum.GetNames(typeof(Unit));
    }

    //Metod för om det sker något fel så ska det visas att det gått snett till
    public static void DisplayInputError()
    {
        MessageBox.Show("Input error");
    }

    public static void NameTryParse(string name)//Ingridiens, namn
    {
        int number;
        bool isNumber = int.TryParse(name, out number);
        if (isNumber == true)
        {
            // == true
        }
        else
        {
            // == false
        }
    }

    public static Recipe GetRecipeFromListView(ListView listView)
    {
        if (listView.SelectedItem != null)
        {
            ListViewItem selectedItem = listView.SelectedItem as ListViewItem;
            Recipe selectedRecipe = selectedItem.Tag as Recipe;
            return selectedRecipe;
        }
        return null;
    }

    public static void LoadLviewIngredients<T>(List<T> list, ListView lview)
    {
        lview.Items.Clear();
        foreach (var item in list)
        {
            lview.Items.Add(item.ToString());
        }
    }

    public static void LoadLview<T>(List<T> list, ListView lview)
    {
        lview.Items.Clear();
        foreach (var item in list)
        {
            lview.Items.Add(item.ToString());
        }
    }

    //Laddar listviewn med recept för framsidan med alla recept
    public static void LoadLview(ListView listView, UnitOfWork unitOfWork)
    {
        foreach (var recipe in unitOfWork.Recipes.GetAll())
        {
            WindowManager.AddLvItemToLv(WindowManager.ConvertToListViewItem(recipe, $"{recipe.Name}"), listView);
        }
    }

    //Tar in ett object och returnerar ett item som ska kunnas läggas till i en listview
    public static ListViewItem ConvertToListViewItem(Object obj, string objContent)
    {
        ListViewItem lvItem = new();
        lvItem.Tag = obj;
        lvItem.Content = objContent;
        return lvItem;
    }

    //Ska ladda alla ingridienser
    public static void LoadIngredients(ListView listView, List<Ingredient> ingredients)
    {
        listView.Items.Clear();
        foreach (var ingredient in ingredients)
        {
            WindowManager.AddLvItemToLv(WindowManager.ConvertToListViewItem(ingredient, $"{ingredient.Name}"), listView);
        }
    }

    //tar alla recept inklusive deras ingridienser
    public static List<Recipe> GetAllRecipesWithIngredients()
    {
        return _context.Recipes.Include(r => r.Ingredients).ToList();
    }
}
