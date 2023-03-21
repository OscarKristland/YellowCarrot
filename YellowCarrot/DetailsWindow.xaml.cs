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
using UIViewHandler;
using YellowCarrot.Data;
using YellowCarrot.Manager;
using YellowCarrot.Models;
using YellowCarrot.Repositories;

namespace YellowCarrot;

/// <summary>
/// Interaction logic for DetailsWindow.xaml
/// </summary>
public partial class DetailsWindow : Window
{
    private Recipe? _recipe;
    private List<Ingredient> CurrentIngredients = new();
    private List<Ingredient> IngredientsToAdd = new();
    private List<Ingredient> IngredientsToRemove = new();

    //Fönster som körs när ett recept följer med.
    public DetailsWindow(Recipe? selectedRecipe)
    {
        InitializeComponent();
        _recipe = selectedRecipe;
        DisableEditorialButtons();
        WindowManager.LoadAllUnits(cboUnit);
    }

    private void DisableEditorialButtons()
    {
        btnSaveChanges.IsEnabled = false;
        btnDeleteIngredient.IsEnabled = false;
        btnAddIngredient.IsEnabled = false;
        txtRecipeName.IsEnabled = false;
    }

    private void btnSaveChanges_Click(object sender, RoutedEventArgs e)
    {
        using (var unitOfWork = new UnitOfWork(new AppDbContext()))
        {
            //Ta bort
            foreach (var ingredientToRemove in IngredientsToRemove)
            {
                unitOfWork.Ingredients.Remove(ingredientToRemove);
            }
            //Lägg till
            foreach (var ingredientToAdd in IngredientsToAdd)
            {
                unitOfWork.Ingredients.Remove(ingredientToAdd);
            }
            //Är namnet samma some innan?
            if (_recipe.Name != txtRecipeName.Text)
            {
                _recipe.Name = txtRecipeName.Text;
            }
            unitOfWork.Complete();
            MessageBox.Show($"You have edited the {_recipe.Name} recipe!!!!!");
            ViewHandler.OpenNewWindow(typeof(MainWindow));
            ViewHandler.CloseWindow(this);
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
        txtRecipeName.IsEnabled = true;
    }
    //Lägger till en ingrediens till receptet
    private void btnAddIngredient_Click(object sender, RoutedEventArgs e)
    {
        if (int.TryParse(txtIngredientQuantity.Text, out int quantity))
        {
            WindowManager.CreateIngredient(txtIngredientQuantity.Text, quantity, cboUnit.SelectedItem.ToString());
        }
    }
    //Tar bort en ingrediens från receptet
    private void btnDeleteIngredient_Click(object sender, RoutedEventArgs e)
    {
    }

    private void lvIngredientList_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }

    private void cboUnit_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }
}
