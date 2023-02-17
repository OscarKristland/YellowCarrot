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
    //hantera alla fönster, alltså alla klick,

    public static Recipe CreateRecipe(string name,List<Ingredient>ingredients)
    {
        //namn
        //minst 2 ingredienser
        //tag om det har valts

        Recipe recipe = new();
        recipe.Name = name;
        recipe.Ingredients = ingredients;

        return recipe;
    }

    public static Ingredient AddIngredient(string name, string quantity)
    {
        Ingredient ingredient = new();
        ingredient.Name = name;
        ingredient.Quantity = quantity;
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

    //laddar en listview från en generell lista
    public static void LoadLview<T>(List<T> list, ListView lview)
    {
        lview.Items.Clear();
        foreach (var item in list)
        {
            lview.Items.Add(item.ToString());
        }
    }

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

    //Visar alla recept i en listview
    public static void DisplayRecipes(ListView listView)
    {
        listView.Items.Clear();

        //using (AppDbContext context = new())
        //{
        //    List<Recipe> recipes = new RecipeRepository(context).GetRecipes();
        //    foreach (Recipe recipe in recipes)
        //    {
        //        ListViewItem item = new();

        //        item.Content = recipe.Name;
        //        item.Tag = recipe;

        //        listView.Items.Add(item);
        //    }
        //}

    }

    public static ListViewItem ConvertListViewItem(Object obj)
    {
        ListViewItem lvItem = new();
        lvItem.Tag = obj;
        return lvItem;
    }
}
