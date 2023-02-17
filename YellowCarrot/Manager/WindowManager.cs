using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using YellowCarrot.Data;
using YellowCarrot.Models;
using YellowCarrot.Repositories;

namespace YellowCarrot.Manager;

public static class WindowManager
{
    //hantera alla fönster, alltså alla klick,



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
}
