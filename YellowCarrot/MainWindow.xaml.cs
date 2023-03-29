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
using UIViewHandler;
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
                WindowManager.LoadListView(lvRecipes, unitOfWork);
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
        Recipe selectedRecipe = WindowManager.GetRecipeFromListView(lvRecipes);

        using (var unitOfWork = new UnitOfWork(new AppDbContext()))
        {
            selectedRecipe = unitOfWork.Recipes.GetRecipeWithIngredients(selectedRecipe.Id);

            DetailsWindow detailsWindow = new DetailsWindow(selectedRecipe);
            unitOfWork.Complete();
            detailsWindow.Show();
            this.Close();

        }
    }
    //Ta bort recept, varningsmeddelande först sen en bekräftelse
    private void btnDelete_Click(object sender, RoutedEventArgs e)
    {
        if (lvRecipes.SelectedItem != null)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to delete the selected recipe?", "Warning!", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                using (var unitOfWork = new UnitOfWork(new AppDbContext()))
                {
                    unitOfWork.Recipes.Remove(lvRecipes.SelectedItem as Recipe);
                    unitOfWork.Complete();
                    WindowManager.DisplaySuccessMessage("The Recipe was removed!");
                    WindowManager.LoadListView(lvRecipes, unitOfWork);
                }
            }
        }
        else
        {
            WindowManager.DisplayErrorMessage("You must select a Recipe to remove!");
        }
    }
    
    
    //Check för att se att ett recept har valts och att det inte går att komma till detailswindow utan att valt ett recept.
    private void lvRecipes_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (lvRecipes.SelectedItem != null)
        {
            ViewHandler.EnableElements(btnDelete, btnDetails);
        }
        else
        {
            ViewHandler.DisableElements(btnDelete, btnDetails);
        }
    }
    //Stänger ner programmet
    private void btnQuit_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
}
