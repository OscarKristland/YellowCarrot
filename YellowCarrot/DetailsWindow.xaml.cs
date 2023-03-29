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
    private List<Ingredient> Ingredients = new();
    //private List<Ingredient> currentIngredients = new();
    //private List<Ingredient> ingredientsToAdd = new();
    //private List<Ingredient> ingredientsToRemove = new();

    //Fönster som körs när ett recept följer med.
    public DetailsWindow(Recipe? selectedRecipe)
    {
        InitializeComponent();
        _recipe = selectedRecipe;
        DisableEditorialButtons();
        WindowManager.LoadUnitsToComboBox(cboUnit);
        WindowManager.LoadLviewIngredients(_recipe!.Ingredients, lvIngredientList);
    }

    private void DisableEditorialButtons()
    {
        btnSaveChanges.IsEnabled = false;
        btnDeleteIngredient.IsEnabled = false;
        btnAddIngredient.IsEnabled = false;
        txtRecipeName.IsEnabled = false;
        txtIngredientQuantity.IsEnabled = false;
        txtIngredient.IsEnabled = false;
        cboUnit.IsEnabled = false;
    }

    private void btnSaveChanges_Click(object sender, RoutedEventArgs e)
    {
        //using (var unitOfWork = new UnitOfWork(new AppDbContext()))
        //{
        //    //Ta bort
        //    foreach (var ingredientToRemove in ingredientsToRemove)
        //    {
        //        unitOfWork.Ingredients.Remove(ingredientToRemove);
        //    }
        //    //Lägg till
        //    foreach (var ingredientToAdd in ingredientsToAdd)
        //    {
        //        unitOfWork.Ingredients.Remove(ingredientToAdd);
        //    }
        //    //Är namnet samma some innan?
        //    if (_recipe.Name != txtRecipeName.Text)
        //    {
        //        _recipe.Name = txtRecipeName.Text;
        //    }
        //    unitOfWork.Complete();
        //    MessageBox.Show($"You have edited the {_recipe.Name} recipe!!!!!");
        //    ViewHandler.OpenNewWindow(typeof(MainWindow));
        //    ViewHandler.CloseWindow(this);
        //}

        if (WindowManager.Checking(txtRecipeName.Text, Ingredients))
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                Recipe recipe = WindowManager.CreateRecipe(txtRecipeName.Text, Ingredients);
                unitOfWork.Recipes.Add(recipe);
                unitOfWork.Complete();
                MessageBox.Show($"You have edited the {_recipe.Name} recipe!!!!!");

                ViewHandler.OpenNewWindow(typeof(MainWindow));
                ViewHandler.CloseWindow(this);
            }
        }
        else
        {
            WindowManager.DisplayInputError("An error occurred, please try again.");
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
        txtIngredientQuantity.IsEnabled = true;
        txtIngredient.IsEnabled = true;
        cboUnit.IsEnabled = true;
    }
    //Lägger till en ingrediens till receptet
    private void btnAddIngredient_Click(object sender, RoutedEventArgs e)
    {
        //if (int.TryParse(txtIngredientQuantity.Text, out int quantity))
        //{
        //    Ingredient newIngredient = WindowManager.CreateIngredient(txtIngredientQuantity.Text, quantity, cboUnit.SelectedItem.ToString());
        //    ingredientsToAdd.Add(newIngredient);
        //    currentIngredients.Add(newIngredient);

        //    WindowManager.LoadListView<Ingredient>(currentIngredients, lvIngredientList);
        //}
        if (!String.IsNullOrEmpty(txtIngredient.Text) && !String.IsNullOrEmpty(txtIngredientQuantity.Text))
        {
            if (int.TryParse(txtIngredientQuantity.Text, out int quantity))
            {
                //Om tryparsen lyckas så kommer den att returnera true,
                //genom att spara värdet från quantity.text i parametern quantity.
                Ingredient ingredient = WindowManager.CreateIngredient(txtIngredient.Text,
                                                                       quantity,
                                                                       cboUnit.SelectedItem.ToString()!);
                Ingredients.Add(ingredient);
                ClearIngredientInputs();
                WindowManager.LoadLviewIngredients(Ingredients, lvIngredientList);
            }
        }
        else
        {
            WindowManager.DisplayInputError("An error occured, please try again.");
        }
    }
    //Tar bort en ingrediens från receptet
    private void btnDeleteIngredient_Click(object sender, RoutedEventArgs e)
    {
        if (lvIngredientList.SelectedItem != null)
        {
            //lvIngredientList.Items.Remove(lvIngredientList.SelectedItem);
            //KOlla i aleks och hans Yellowcarrot

            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                unitOfWork.Ingredients.Remove(lvIngredientList.SelectedItem as Ingredient);
            }
        }
        else if (lvIngredientList.SelectedItem == null)
        {
            WindowManager.DisplayInputError("Please select an igredient to remove!");
        }
    }

    private void ClearIngredientInputs()
    {
        txtIngredient.Clear();
        txtIngredientQuantity.Clear();

    }
}
