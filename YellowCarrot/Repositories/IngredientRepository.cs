using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowCarrot.Data;
using YellowCarrot.Interface;
using YellowCarrot.Models;

namespace YellowCarrot.Repositories;

public class IngredientRepository : Repository<Ingredient>, IIngredientRepository
{
    public readonly AppDbContext _context;

    public IngredientRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
}
