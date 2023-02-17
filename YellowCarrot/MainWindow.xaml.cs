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
using YellowCarrot.Models;
using YellowCarrot.Repositories;

namespace YellowCarrot;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public List<Recipe> Recipes { get; set; }

    public MainWindow()
    {
        InitializeComponent();
        WindowStartupLocation = WindowStartupLocation.CenterScreen;

        //using (AppDbContext context = new())
        //{
        //    //btnDetails.IsEnabled = false;
        //    //btnDelete.IsEnabled = false;
        //    //DisplayRecipes();
        //    //context.SaveChanges();
        //}
        
    }

    

    //Öppnar ett fönster där användaren ska kunna lägga till ett recept
    private void AddRecipe_Click(object sender, RoutedEventArgs e)
    {
        AddRecipWindow addRecipWindow= new AddRecipWindow();
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
    //Stänger ner programmet
    private void btnQuit_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
    //Ta bort recept, varningsmeddelande först sen en bekräftelse
    private void btnDelete_Click(object sender, RoutedEventArgs e)
    {
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

    private void lvRecipes_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        btnDelete.IsEnabled = true;
        btnDetails.IsEnabled = true;
    }
}
