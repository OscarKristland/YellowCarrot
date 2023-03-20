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
using YellowCarrot.Models;
using YellowCarrot.Repositories;

namespace YellowCarrot;

/// <summary>
/// Interaction logic for DetailsWindow.xaml
/// </summary>
public partial class DetailsWindow : Window
{
    private readonly Recipe? _recipe;

    //Fönster som körs när ett recept följer med.
    public DetailsWindow(Recipe? selectedRecipe)
    {
        InitializeComponent();
        _recipe = selectedRecipe;

        btnSaveChanges.IsEnabled = false;
        btnDeleteIngredient.IsEnabled = false;
        btnAddIngredient.IsEnabled = false;
        txtNewRecipeName.IsEnabled = false;


        WindowStartupLocation = WindowStartupLocation.CenterScreen;
        //lblShowRecipeName.Content = selectedRecipe.Name;

        UpdateUi();
    }

    //Uppdaterar Listviewn
    private void UpdateUi()
    {
        lvIngredientList.Items.Clear();

        if (_recipe != null)
        {
            lblShowRecipeName.Content = _recipe.Name;

            foreach (Ingredient ingredient in _recipe.Ingredients)
            {
                ListViewItem item = new ListViewItem();

                item.Content = ingredient.DisplayString;
                item.Tag = ingredient;

                lvIngredientList.Items.Add(item);
            }
        }
    }
    private void btnSaveChanges_Click(object sender, RoutedEventArgs e)
    {
        if (txtNewRecipeName.Text.Length > 3)
        {
            string newRecipeName = txtNewRecipeName.Text;

            if (newRecipeName != null)
            {
                _recipe!.Name = newRecipeName;

                using(AppDbContext context = new())
                {
                    //new RecipeRepository(context).UpdateRecipe(_recipe);
                    MessageBox.Show("Your recipe has been succesfully changed");
                    context.SaveChanges();
                }
            }
        }
    }
    //Ta användaren tillbaka till main window
    private void btnCancel_Click(object sender, RoutedEventArgs e)
    {
        MainWindow mainWindow = new();
        mainWindow.Show();
        Close();
    }
    //Ändra ett recept som synliggör att användaren kan spara receptet
    private void btnEdit_Click(object sender, RoutedEventArgs e)
    {
        btnSaveChanges.IsEnabled = true;
        btnEdit.IsEnabled = false;
        btnDeleteIngredient.IsEnabled = true;
        btnAddIngredient.IsEnabled = true;
        txtNewRecipeName.IsEnabled = true;
    }
    //Lägger till en ingrediens till receptet
    private void btnAddIngredient_Click(object sender, RoutedEventArgs e)
    {
        var ingredientName = txtIngredient.Text;
        var ingredientQuantity = txtIngredientQuantity.Text;

        Ingredient ingredient = new();
        ingredient.Name = ingredientName;
        ingredient.Quantity = ingredientQuantity;
        ingredient.RecipeId = _recipe.RecipeId;
        ingredient.Recipe = _recipe;

        _recipe.Ingredients.Add(ingredient);
        using(AppDbContext context = new())
        {
            //new IngredientRepository(context).AddIngredient(ingredient);
            //new RecipeRepository(context).AddIngredientToRcipe(ingredient);
        }

        UpdateUi();
    }
    //Tar bort en ingrediens från receptet
    private void btnDeleteIngredient_Click(object sender, RoutedEventArgs e)
    {
        //ListViewItem? selectedItem = lvIngredientList.SelectedItem as ListViewItem;

        //Ingredient? selectedIngredient = selectedItem?.Tag as Ingredient;

        //if(selectedItem != null)
        //{
        //    _recipe?.Ingredients.Remove(selectedIngredient!);

        //    using (AppDbContext context = new())
        //    {
        //        new IngredientRepository(context).IngredientToRemove(selectedIngredient!);
        //        context.SaveChanges();
        //        MessageBox.Show("Item successfully removed!");
        //    }
        //}

        //UpdateUi();
    }

    private void lvIngredientList_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }
}
