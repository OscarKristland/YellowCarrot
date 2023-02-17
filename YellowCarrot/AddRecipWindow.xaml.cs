using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using YellowCarrot.Data;
using YellowCarrot.Manager;
using YellowCarrot.Models;
using YellowCarrot.Repositories;

namespace YellowCarrot;

/// <summary>
/// Interaction logic for AddRecipWindow.xaml
/// </summary>
public partial class AddRecipWindow : Window
{
    private List<Ingredient> Ingredients = new();

    //private List<Tag> Tags = new();

    public AddRecipWindow()
    {
        InitializeComponent();
        WindowStartupLocation = WindowStartupLocation.CenterScreen;
        WindowManager.LoadCboBox();
        
    }

    //Tas tillbaka till mainwindow
    private void btnCancel_Click(object sender, RoutedEventArgs e)
    {
        MainWindow mainWindow= new MainWindow();
        mainWindow.Show();
        Close();
    }
    //lägg till recept
    private void btnAdd_Click(object sender, RoutedEventArgs e)
    {
        if (WindowManager.Checking(txtRecipeName.Text, Ingredients))
        {
            using (var UnitOfWork = new UnitOfWork(new AppDbContext()))
            {
                Recipe recipe = WindowManager.CreateRecipe(txtRecipeName.Text, Ingredients);
                UnitOfWork.Recipes.Add(recipe);
                UnitOfWork.Complete();
            }
        }
        else
        {
            WindowManager.DisplayInputError();
        }
    }
    //Lägg till ingredienser
    private void btnAddIngredient_Click(object sender, RoutedEventArgs e)
    {



        //if (String.IsNullOrEmpty(txtIngredient.Text))
        //{
        //    MessageBox.Show("The recipe has to atleast have an ingredient");
        //}
        //else if (String.IsNullOrEmpty(txtIngredientQuantity.Text))
        //{
        //    MessageBox.Show("Please fill in the quantity");
        //}
        //else
        //{
        //    ClearUi();
        //}
    }

    private void ClearUi()
    {
        txtIngredient.Clear();
        txtIngredientQuantity.Clear();

    }
}
