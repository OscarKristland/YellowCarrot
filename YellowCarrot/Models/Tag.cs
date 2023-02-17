using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowCarrot.Models;

public class Tag
{
    [Key]
    public int TagId { get; set; }
    public List<Recipe> Recipes { get; set; } = new();
    [MaxLength(50)]
    public string TagName { get; set; } = null!;
    //Many to one relationship, mellan tag och recept
}
