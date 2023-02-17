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
        btnAdd.IsEnabled = false;
        btnAddIngredient.IsEnabled = false;
        UpdateUi();
    }
    //Uppdaterar comboboxen och rensar Listviewn
    private void UpdateUi()
    {
        lvIngredientList.Items.Clear();
        cboTag.Items.Clear();

        using(AppDbContext context = new())
        {
            List<Tag> tags = context.Tags.ToList();

            foreach(Tag tag in tags)
            {
                ComboBoxItem cboItem = new();
                cboItem.Content = tag.TagName;
                cboItem.Tag = tag;

                cboTag.Items.Add(cboItem); 
            }
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



        if (String.IsNullOrEmpty(txtRecipeName.Text))
        {
            MessageBox.Show("Please fill in the name of your recipe");
        }
        else if (Ingredients.Count == 0)
        {
            MessageBox.Show("Please add atleast one ingredient");
        }
        else if (cboTag.SelectedItem == null)
        {
            MessageBox.Show("Please select a tag for the recipe");
        }
        else
        {
            string recipeName = txtRecipeName.Text;
            string ingredient1 = txtIngredient.Text;
            string quantityIngredient1 = txtIngredientQuantity.Text;

            using (AppDbContext context = new())
            {
                

                

                context.SaveChanges();
            }
            txtRecipeName.Clear();
        }
    }
    //Lägg till ingredienser
    private void btnAddIngredient_Click(object sender, RoutedEventArgs e)
    {
        if (String.IsNullOrEmpty(txtIngredient.Text))
        {
            MessageBox.Show("The recipe has to atleast have an ingredient");
        }
        else if (String.IsNullOrEmpty(txtIngredientQuantity.Text))
        {
            MessageBox.Show("Please fill in the quantity");
        }
        else
        {
            ClearUi();
        }
    }

    private void ClearUi()
    {
        txtIngredient.Clear();
        txtIngredientQuantity.Clear();

    }
}
