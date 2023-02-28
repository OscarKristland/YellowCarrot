using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using YellowCarrot.Data;
using YellowCarrot.Manager;
using YellowCarrot.Models;
using YellowCarrot.Repositories;

namespace YellowCarrot;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    

    public MainWindow()
    {
        InitializeComponent();
        WindowStartupLocation = WindowStartupLocation.CenterScreen;

        using (AppDbContext context = new())
        {
            using (var unitOfWork = new UnitOfWork(context))
            {
                
                WindowManager.LoadLview(unitOfWork.Recipes.GetAll()
                                                          .ToList(), 
                                                          lvRecipes);

                //List<Recipe> Recipes = unitOfWork.Recipes.GetAll().ToList();
                //listan på recept, recipes(Recipes), den vill sedan veta var den ska stoppas in, alltså i listviewn som kallas lvRecipes.
                //WindowManager.LoadLview(Recipes, lvRecipes);
            }
        }
    }



    //Öppnar ett fönster där användaren ska kunna lägga till ett recept
    private void AddRecipe_Click(object sender, RoutedEventArgs e)
    {
        AddRecipWindow addRecipWindow = new AddRecipWindow();
        addRecipWindow.Show();
        Close();
    }

    //Visar det valda receptet i ett nytt fönster
    private void btnDetails_Click(object sender, RoutedEventArgs e)
    {
    //    using (AppDbContext context = new())
    //    {
    //        ListViewItem selectedItem = lvRecipes.SelectedItem as ListViewItem;

    //        if(selectedItem == null)
    //        {
    //            MessageBox.Show("Please select a recipe.");
    //        }
    //        else
    //        {
    //            Recipe selectedRecipe = selectedItem.Tag as Recipe;
    //            DetailsWindow detailsWindow = new DetailsWindow(selectedRecipe);
    //            detailsWindow.Show();
    //            Close();
    //        }
    //    }

    }
    //Ta bort recept, varningsmeddelande först sen en bekräftelse
    private void btnDelete_Click(object sender, RoutedEventArgs e)
    {

        //Delete funktion
        if (lvRecipes.SelectedItem != null)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to delete the selected recipe?", "Warning!", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                using (AppDbContext context = new())
                {
                    using (var unitOfWork = new UnitOfWork(context))
                    {
                        //Vill ha en complete efter det att jag tagit bort ett recept
                        context.Recipes.Where(x => x == unitOfWork.Recipes).FirstOrDefault();
                        context.SaveChanges();
                    }
                }
            }
        }
        else
        {
            MessageBox.Show("Error");
        }

        //ListViewItem selectedItem = lvRecipes.SelectedItem as ListViewItem;

        //if(selectedItem != null)
        //{
        //    Recipe recipe = selectedItem.Tag as Recipe;

        //    MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to delete the selected recipe?", "Warning!", MessageBoxButton.YesNo);
        //    if(messageBoxResult == MessageBoxResult.Yes)
        //    {
        //        using (AppDbContext context = new())
        //        {
        //            recipe = context.Recipes.Where(x => x == recipe).FirstOrDefault();
        //            context.SaveChanges();
        //        }
        //        DisplayRecipes();
        //    }

        //}
    }
    
    
    //Check för att se att ett recept har valts och att det inte går att komma till detailswindow utan att valt ett recept.
    private void lvRecipes_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

        //ListViewItem? selectedItem = lvRecipes.SelectedItem as ListViewItem;
        if (lvRecipes.SelectedItem != null)
        {
            btnDelete.IsEnabled = true;
            btnDetails.IsEnabled = true;
        }
        else
        {
            btnDelete.IsEnabled = false;
            btnDetails.IsEnabled = false;
        }
    }
    //Stänger ner programmet
    private void btnQuit_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
}
