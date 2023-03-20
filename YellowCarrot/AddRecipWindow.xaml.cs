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

        using (var unitOfWork = new UnitOfWork(new AppDbContext()))
        {
            //List<Tag> tags = new();
            //tags = unitOfWork.Tags.GetAll().ToList();
            //WindowManager.LoadCboBox(tags, cboTag);

            //Säger samma sak som koden ovan. Fungerar på samma sätt som den i main window för att ladda listviewn
            WindowManager.LoadCboBox(unitOfWork.Tags.GetAll().ToList(), cboTag);
        }
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
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                Recipe recipe = WindowManager.CreateRecipe(txtRecipeName.Text, Ingredients);
                unitOfWork.Recipes.Add(recipe);
                unitOfWork.Complete();
                MessageBox.Show("Recipe Added");
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Close();
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
        if (!String.IsNullOrEmpty(txtIngredient.Text) && !String.IsNullOrEmpty(txtIngredientQuantity.Text))
        {
            Ingredient ingredient = WindowManager.AddIngredient(txtIngredient.Text, txtIngredientQuantity.Text);
            Ingredients.Add(ingredient);
            ClearIngredientInputs();
            WindowManager.LoadLviewIngredients(Ingredients, lvIngredientList);

            //using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            //{
            //    Ingredient ingredient = WindowManager.AddIngredient(txtIngredient.Text, txtIngredientQuantity.Text);
            //    unitOfWork.Ingredients.Add(ingredient);
            //    unitOfWork.Complete();
            //}
        }
        else

        WindowManager.DisplayInputError();


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

    private void ClearIngredientInputs()
    {
        txtIngredient.Clear();
        txtIngredientQuantity.Clear();

    }

    private void btnDeleteIngredient_Click(object sender, RoutedEventArgs e)
    {
        ListViewItem selectedItem = lvIngredientList.SelectedItem as ListViewItem;

        if (selectedItem != null)
        {
            ListViewItem selectedItemToDelete = (ListViewItem)lvIngredientList.SelectedItems[0];
            lvIngredientList.Items.Remove(selectedItemToDelete);
        }
        else if (selectedItem == null)
        {
            MessageBox.Show("Error");
        }

        
    }

    ///if (int.TryParse(txtIngredientQuantity.Text, out int quantity))
    //{
    //     Ingredient ingredient = WindowManager.AddIngredient(txtIngredient.Text, txtIngredientQuantity.Text);
    //}

}
