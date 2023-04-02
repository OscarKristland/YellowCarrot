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

    public AddRecipWindow()
    {
        InitializeComponent();
        WindowStartupLocation = WindowStartupLocation.CenterScreen;

        using (var unitOfWork = new UnitOfWork(new AppDbContext()))
        {
            WindowManager.LoadComboBox(unitOfWork.Tags.GetAll().ToList(), cboTag);
            WindowManager.LoadUnitsToComboBox(cboUnit);
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
    private void btnSaveRecipe_Click(object sender, RoutedEventArgs e)
    {
        if (WindowManager.CheckingIfRecipeIsEmpty(txtRecipeName.Text, Ingredients))
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
            WindowManager.DisplayErrorMessage("A recipe needs to have at least one ingredient...");
        }
    }
    //Lägg till ingredienser
    private void btnAddIngredient_Click(object sender, RoutedEventArgs e)
    {
        if (!String.IsNullOrEmpty(txtIngredient.Text))
        {
            if (!String.IsNullOrEmpty(txtIngredientQuantity.Text))
            {
                if (cboUnit.SelectedIndex != -1)
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
                                                                                                    $"{ingredient.Unit}"), 
                                                                                                    lvIngredientList);
                        Ingredients.Add(ingredient);
                        ClearIngredientInputs();

                        WindowManager.DisplaySuccessMessage($"You have successfully added {ingredient.Name}");
                    }
                    else
                    {
                        WindowManager.DisplayInputError("Something went wrong, please try again.");
                    }
                }
                else
                {
                    WindowManager.DisplayInputError("You need to fill in what unit you wish to use!");
                }
            }
            else
            {
                WindowManager.DisplayInputError("You need to specify the quantity of an ingredient!");
            }
        }
        else
        {
            WindowManager.DisplayInputError("An ingredient needs a name");
        }
        //if (!String.IsNullOrEmpty(txtIngredient.Text) && !String.IsNullOrEmpty(txtIngredientQuantity.Text) && cboUnit.SelectedIndex != -1)
        //{
        //    if (int.TryParse(txtIngredientQuantity.Text, out int quantity))
        //    {
        //        //Om tryparsen lyckas så kommer den att returnera true,
        //        //genom att spara värdet från quantity.text i parametern quantity.
        //        Ingredient ingredient = WindowManager.CreateIngredient(txtIngredient.Text, 
        //                                                               quantity, 
        //                                                               cboUnit.SelectedItem.ToString()!);
        //        Ingredients.Add(ingredient);
        //        ClearIngredientInputs();

        //        WindowManager.AddLvItemToLv(WindowManager.ConvertToListViewItem(ingredient, $"{ingredient.Name} | {ingredient.Quantity} | {ingredient.Unit}"), lvIngredientList);

        //        //ListViewItem lvItem = WindowManager.ConvertToListViewItem(ingredient, $"{ingredient.Name} | {ingredient.Quantity} | {ingredient.Unit}");
        //        //WindowManager.AddLvItemToLv(lvItem, lvIngredientList);

        //        //WindowManager.AddLvItemToLv(
        //        //WindowManager.ConvertToListViewItem(ingredient, ingredient.Name), lvIngredientList);
        //    }
        //}   
        //else 
        //{
        //    WindowManager.DisplayInputError("An error occured, please try again.");
        //}
    }

    private void ClearIngredientInputs()
    {
        txtIngredient.Clear();
        txtIngredientQuantity.Clear();

    }

    private void btnDeleteIngredient_Click(object sender, RoutedEventArgs e)
    {
        if (lvIngredientList.SelectedItem != null)
        {
            //Ingredient selectedIngredient = WindowManager.GetIngredientFromListView(lvIngredientList);

            Ingredient ingredientToRemove = WindowManager.GetIngredientFromListView(lvIngredientList);
            Ingredients.Remove(ingredientToRemove);
            lvIngredientList.Items.Remove(lvIngredientList.SelectedItem);

            //ListViewItem lvItem = lvIngredientList.SelectedItem as ListViewItem;
            //Ingredient? ingreedientToRemove = lvItem.Tag as Ingredient;
            //lvIngredientList.Items.Remove(lvIngredientList.SelectedItem);
            //Ingredients.Remove(ingreedientToRemove);
            
        }
        else if (lvIngredientList.SelectedItem == null)
        {
            WindowManager.DisplayInputError("Please select an igredient to remove!");
        }

        WindowManager.DisplaySuccessMessage("You have successfully removed and ingredient!");
    }
}
