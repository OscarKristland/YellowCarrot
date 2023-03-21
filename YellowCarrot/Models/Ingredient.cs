using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowCarrot.Models;

public class Ingredient
{
    [Key]
    public int IngredientId { get; set; }
    //maxlength viktig, för RAM:ets skull. Annars reserveras halva minnet när informationen ska hämtas.
    [MaxLength(50)]
    public string Name { get; set; } = null!;
    [MaxLength(50)]
    public int Quantity { get; set; }
    public string Units { get; set; }
    [MaxLength(50)]
    public string DisplayString => $"{Name} / {Quantity} / {Units}";
    public int RecipeId { get; set; }
    public Recipe Recipe { get; set; }
    //one to many relationship mellan recept och ingrediens

    public override string ToString()
    {
        return DisplayString;
    }

}
