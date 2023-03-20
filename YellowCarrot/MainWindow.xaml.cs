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
                WindowManager.LoadLview(lvRecipes, unitOfWork);
                unitOfWork.Complete();
            }
        }

        //using (AppDbContext context = new())
        //{
        //    using (var unitOfWork = new UnitOfWork(context))
        //    {
        //        WindowManager.LoadLview(unitOfWork.Recipes.GetAll()
        //                                                  .ToList(),
        //                                                  lvRecipes);
        //        unitOfWork.Complete();

        //        List<Recipe> Recipes = unitOfWork.Recipes.GetAll().ToList();
        //        //listan på recept, recipes(Recipes), den vill sedan veta var den ska stoppas in, alltså i listviewn som kallas lvRecipes.
        //        WindowManager.LoadLview(Recipes, lvRecipes);
        //    }
        //}
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
        Recipe recipe = WindowManager.GetRecipeFromListView(lvRecipes);

        using (var unitOfWork = new UnitOfWork(new AppDbContext()))
        {
            //Receptet som har valts ska skickas med till detailsWindow. Receptet och ingredienser.

            //CHecken görs genom windowmanager, alltså ska vi bara kalla på windowmanager och vilken metod i vill ska köras.

            recipe = unitOfWork.Recipes.GetRecipeWithIngredients(recipe.RecipeId);
            WindowManager.LoadIngredients(lvRecipes, recipe.Ingredients);
            unitOfWork.Complete();





            //if(lvRecipes.SelectedItem != null)
            //{
            //    Recipe? selectedRecipe = lvRecipes.SelectedItem as Recipe;
            //    DetailsWindow detailsWindow = new DetailsWindow(selectedRecipe);
            //    detailsWindow.Show();
            //    unitOfWork.Complete();
            //    Close();

            //    //Receptet åker inte med
            //}
            //else
            //{
            //    MessageBox.Show("Smth went wrong");
            //}

        }

        // Open a new window with the correct data loaded from the selected recipe,
        // ( use the other windows constructor to accept a recipe object,
        // then in the constructor set the UI-Elements content to the corresponding recipes properties),
        // You will need to use the .Tag property to get the correct recipe object,
        // then store it in a variable and pass that variable in the window constructor



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

        // Removes the selected item from the listview.
        // Take the listviews selected item, cast it to the correct class object using the
        // .Tag property and store it inside a variable.
        // Call the Remove function from the correct repository with the variable as input parameter,
        // remember to use unitOfWork and Update in the end.

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
                        //Recipe selectedRecipe = (Recipe)((ListViewItem)lvRecipes.SelectedItem).Tag;

                        //ListViewItem selectedRecipe = lvRecipes.SelectedItem as ListViewItem;
                        //if (selectedRecipe == null)
                        //{
                        //    context.
                        //}
                        ////Vill ha en complete efter det att jag tagit bort ett recept

                        ////context.Recipes.Remove(selectedRecipe);

                        //Recipe recipe = WindowManager.DeleteRecipe(selectedRecipe);
                        //unitOfWork.Recipes.Remove(selectedRecipe);
                        //unitOfWork.Complete();
                        //unitOfWork.Complete();
                    }
                }
            }
        }
        else
        {
            MessageBox.Show("Error");
        }
    }
    
    
    //Check för att se att ett recept har valts och att det inte går att komma till detailswindow utan att valt ett recept.
    private void lvRecipes_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        // Add a conditional/switch check to see if the new selectedItem in the listview is not null,
        // if it's not toggle buttons.IsEnabled to true, else toggle buttons.IsEnabled to false.
        // This catches a bug that makes the buttons enabled,
        // if there only is one recipe in the listview and that listview is then deleted.

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
