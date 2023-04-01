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
using YellowCarrot.Interface;
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
    private List<Ingredient> currentIngredients = new();
    private List<Ingredient> ingredientsToAdd = new();
    private List<Ingredient> ingredientsToRemove = new();

    //Fönster som körs när ett recept följer med.
    public DetailsWindow(Recipe? selectedRecipe)
    {
        using (var unitOfWork = new UnitOfWork(new AppDbContext()))
        {
            InitializeComponent();
            _recipe = selectedRecipe;
            DisableEditorialButtons();
            WindowManager.LoadUnitsToComboBox(cboUnit);
            WindowManager.LoadIngredients(lvIngredientList, _recipe.Ingredients);
            lblRecipeDetails.Content = unitOfWork.Recipes.FindById(_recipe.Id).Name;
            currentIngredients = _recipe.Ingredients;
            txtRecipeName.Text = _recipe.Name;
        }
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
        //När man ska köra delete, så köra en remove från currentlistan
        //lägger man till i remove listan

        //under add så köra en add till ingredientsToAdd
        //lägga till i currentingredients

        if (WindowManager.CheckingIfRecipeIsEmpty(txtRecipeName.Text, currentIngredients))
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                //Ta bort
                if(ingredientsToRemove.Count > 0)
                {
                    foreach (var ingredientToRemove in ingredientsToRemove)
                    {
                        //Denna går in i databasen och tar bort dem
                        //unitOfWork.Ingredients.FindById(ingredientToRemove.Id);
                        unitOfWork.Ingredients.Remove(ingredientToRemove);
                    }
                }
                //Lägg till
                foreach (var ingredientToAdd in ingredientsToAdd)
                {
                    _recipe.Ingredients.Add(ingredientToAdd);
                }
                //Är namnet samma some innan?
                if (_recipe.Name != txtRecipeName.Text)
                {
                    _recipe.Name = txtRecipeName.Text;
                }
                //unitOfWork.Recipes.Remove(_recipe);
                //Recipe recipe = WindowManager.CreateRecipe(txtRecipeName.Text, currentIngredients);
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
        if (!String.IsNullOrEmpty(txtIngredient.Text) && !String.IsNullOrEmpty(txtIngredientQuantity.Text) && cboUnit.SelectedIndex != -1)
        {
            if (int.TryParse(txtIngredientQuantity.Text, out int quantity))
            {
                //Om tryparsen lyckas så kommer den att returnera true,
                //genom att spara värdet från quantity.text i parametern quantity.
                Ingredient ingredient = WindowManager.CreateIngredient(txtIngredient.Text,
                                                                       quantity,
                                                                       cboUnit.SelectedItem.ToString()!);
                
                WindowManager.AddLvItemToLv(WindowManager.ConvertToListViewItem(ingredient, $"{ingredient.Name} | " +
                                                                                            $"{ingredient.Quantity} | " +
                                                                                            $"{ingredient.Unit}"), lvIngredientList);
                ingredientsToAdd.Add(ingredient);
                currentIngredients.Add(ingredient);
                ClearIngredientInputs();
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
            Ingredient selectedIngredient = WindowManager.GetIngredientFromListView(lvIngredientList);
                
            if (selectedIngredient != null)
            {
                ListViewItem lvItem = lvIngredientList.SelectedItem as ListViewItem;
                Ingredient? ingredientToRemove = lvItem.Tag as Ingredient;
                ingredientsToRemove.Add(ingredientToRemove);
                currentIngredients.Remove(ingredientToRemove);
                lvIngredientList.Items.Remove(ingredientToRemove);
                WindowManager.LoadIngredients(lvIngredientList, currentIngredients);
            }
            else
            {
                throw new ArgumentNullException(nameof(selectedIngredient), "selected item is not an ingredient object");
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
