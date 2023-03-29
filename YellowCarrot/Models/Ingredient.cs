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
    public int Id { get; set; }
    //maxlength viktig, för RAM:ets skull. Annars reserveras halva minnet när informationen ska hämtas.
    [MaxLength(50)]
    public string Name { get; set; } = null!;
    public int Quantity { get; set; }
    [MaxLength(50)]
    public string Unit { get; set; }
    public int RecipeId { get; set; }
    public Recipe Recipe { get; set; }
    //one to many relationship mellan recept och ingrediens

    public override string ToString()
    {
        return $"{Name} / {Quantity} / {Unit}";
    }

}
