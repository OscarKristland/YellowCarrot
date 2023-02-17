using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowCarrot.Data;
using YellowCarrot.Repositories;

namespace YellowCarrot.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IIngredientRepository Ingredients { get; }
        IRecipeRepository Recipes { get; }
        ITagRepository Tags { get; }
        int Complete();
    }
}
