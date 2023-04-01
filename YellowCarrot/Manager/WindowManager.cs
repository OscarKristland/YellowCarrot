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
        ingredient.Unit = unit;
        return ingredient;
    }

    //Går att skapa recept om det finns fler än 3
    public static bool CheckingIfRecipeIsEmpty(string name, List<Ingredient>ingredients)
    {
        if (ingredients.Count() > 0)
        {
            if (!String.IsNullOrEmpty(name))
            {
                return true;
            }
        }
        return false;
    }

    //laddar en cbobox från en generell lista
    public static void LoadComboBox<T>(List<T> list, ComboBox cbo)
    {
        cbo.Items.Clear();
        foreach (var item in list)
        {
            cbo.Items.Add(item!.ToString());
        }
    }

    public static void LoadUnitsToComboBox(ComboBox comboBox)
    {
        comboBox.ItemsSource = Enum.GetNames(typeof(Unit));
    }

    //Metod för om det sker något fel så ska det visas att det gått snett till eller om det gått rätt till
    public static void DisplayErrorMessage(string errorMessage)
    {
        MessageBox.Show(errorMessage, "Error");
    }
    public static void DisplayInputError(string errorMessage)
    {
        MessageBox.Show(errorMessage, "Input error");
    }

    public static void DisplaySuccessMessage(string outputMessage)
    {
        MessageBox.Show(outputMessage, "Success");
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

    public static Ingredient GetIngredientFromListView(ListView listView)
    {
        if(listView.SelectedItem != null)
        {
            ListViewItem selectedItem = listView.SelectedItem as ListViewItem;
            Ingredient selectedIngredient = selectedItem.Tag as Ingredient;
            return selectedIngredient;
        }
        return null;
    }

   
    public static void LoadListView<T>(List<T> list, ListView lview)
    {
        lview.Items.Clear();
        foreach (var item in list)
        {
            lview.Items.Add(item.ToString());
        }
    }

    //Laddar listviewn med recept för framsidan med alla recept
    public static void LoadListView(ListView listView, UnitOfWork unitOfWork)
    {
        listView.Items.Clear();
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
            WindowManager.AddLvItemToLv(WindowManager.ConvertToListViewItem(ingredient, $"{ingredient.Name} / {ingredient.Quantity} / {ingredient.Unit}"), listView);
        }
    }

    //hantera alla fönster, alltså alla klick//
    public static void AddLvItemToLv(ListViewItem lvItem, ListView listView)
    {
        listView.Items.Add(lvItem);
    }

    //tar alla recept inklusive deras ingridienser
    public static List<Recipe> GetAllRecipesWithIngredients()
    {
        return _context.Recipes.Include(r => r.Ingredients).ToList();
    }
}
