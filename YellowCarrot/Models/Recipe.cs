using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowCarrot.Models;

public class Recipe
{
    [Key]
    public int RecipeId { get; set; }
    [MaxLength(50)]
    public string Name { get; set; } = null!;
    public List<Ingredient> Ingredients { get; set; } = new();
    public int TagId { get; set; }

}
